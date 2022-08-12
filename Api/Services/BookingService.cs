using LinkDev.EgyptianRecipes.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.EgyptianRecipes.Services;

public class BookingService
{
    private readonly RestaurantContext _context;
    private const string SelectedTimeInvalid = "Selected time is invalid";
    private const string TimeConfigurationNotFound = "Branch opening and closing time was not configured";


    public BookingService(RestaurantContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestedBookingTime">current of user's datetime to start looking for slots after datetime.now</param>
    /// <param name="branchId">branch Id that the user is willing to make a booking in</param>
    /// <returns>the available timeslots</returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<DateTime>> AvailableTimeslotsFromBranches(DateTime requestedBookingTime, int branchId)
    {
        if (requestedBookingTime < DateTime.Now)
            throw new Exception(SelectedTimeInvalid);

        var timingConfiguration = await _context
            .Branches
            .FirstOrDefaultAsync(x => x.Id == branchId);

        if (timingConfiguration == null)
            throw new Exception(TimeConfigurationNotFound);

        //the busy slots
        var bookingsOfTheDay = await _context.Bookings
            .Where(x => x.BookingStartDateTime >= requestedBookingTime)
            .Where(x => x.BookingEndDateTime.TimeOfDay <= timingConfiguration.ClosingTime)
            .ToListAsync();


        List<DateTime> busyTimeSlots = new();

        foreach (var booking in bookingsOfTheDay)
        {
            var timeslots = CalculateTimeslots(booking.BookingStartDateTime.TimeOfDay,
                booking.BookingEndDateTime.TimeOfDay, requestedBookingTime.Date);

            busyTimeSlots.AddRange(timeslots);
        }


        //all time slots since now till the end of the shift
        var currentTillTheClosingTimeSlots = CalculateTimeslots(requestedBookingTime.TimeOfDay,
            timingConfiguration.ClosingTime, requestedBookingTime.Date);


        IEnumerable<DateTime> common = currentTillTheClosingTimeSlots.Intersect(busyTimeSlots).ToList();

        currentTillTheClosingTimeSlots.RemoveAll(x => common.Contains(x));

        return currentTillTheClosingTimeSlots;
    }

    private List<DateTime> CalculateTimeslots(TimeSpan fromTimeSpan, TimeSpan toTimeSpan, DateTime date)
    {
        var timetable = new List<DateTime>();


        var from = date + fromTimeSpan;
        var to = date + toTimeSpan;


        double intervals = 30.0;
        var size = 60.0 / intervals;

        List<double> numbers = new List<double>();

        for (int i = 0; i < size + 1; i++)
        {
            numbers.Add(intervals * i);
        }


        for (int i = 0; i < numbers.Count; i++)
        {
            if (from.Minute > numbers[i] && from.Minute < numbers[i + 1])
            {
                from = from.AddMinutes(numbers[i + 1] - from.Minute);
            }
        }

        bool once = true;

        while (from < to)
        {
            if (once)
            {
                from = from.AddMinutes(0).AddSeconds(-from.Second);
            }
            else
            {
                from = from.AddMinutes(intervals).AddSeconds(-from.Second);
            }

            once = false;

            timetable.Add(from);
        }

        return timetable;
    }
}