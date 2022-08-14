using LinkDev.EgyptianRecipes.Data;
using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.EgyptianRecipes.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepo _repo;
    private const string SelectedTimeInvalid = "Selected time is invalid";
    private const string TimeConfigurationNotFound = "Branch opening and closing time was not configured";


    public BookingService(IBookingRepo repo)
    {
        _repo = repo;
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

        var shiftConfiguration = await _repo.GetBranchShift(branchId);

        if (shiftConfiguration == null)
            throw new Exception(TimeConfigurationNotFound);

        //the busy slots
        var bookingsOfTheDay = await _repo.GetBusyTimeslots(requestedBookingTime, shiftConfiguration.ClosingTime);

        List<DateTime> busyTimeSlots = new();

        foreach (var booking in bookingsOfTheDay)
        {
            var timeslots = CalculateTimeslots(booking.BookingStartDateTime.TimeOfDay,
                booking.BookingEndDateTime.TimeOfDay, requestedBookingTime.Date,true);

            busyTimeSlots.AddRange(timeslots);
        }


        //all time slots since now till the end of the shift

        List<DateTime> currentTillTheClosingTimeSlots;
        if (requestedBookingTime.Date == DateTime.Today)
        {
            //if booking is in the same day then calculate timeslots starting from now
            currentTillTheClosingTimeSlots = CalculateTimeslots(requestedBookingTime.TimeOfDay,
                shiftConfiguration.ClosingTime, requestedBookingTime.Date);
        }
        else
        {            
            //if booking is not in the same day then calculate timeslots starting from open shift time
            currentTillTheClosingTimeSlots = CalculateTimeslots(shiftConfiguration.OpeningTime,
                shiftConfiguration.ClosingTime, requestedBookingTime.Date);
        }


        // IEnumerable<DateTime> common = currentTillTheClosingTimeSlots.Intersect(busyTimeSlots).ToList();
        // currentTillTheClosingTimeSlots.RemoveAll(x => common.Contains(x));

        for (int i = 0; i < busyTimeSlots.Count; i++)
        {
            for (int j = 0; j < currentTillTheClosingTimeSlots.Count; j++)
            {
                if (busyTimeSlots[i].Hour == currentTillTheClosingTimeSlots[j].Hour && busyTimeSlots[i].Minute == currentTillTheClosingTimeSlots[j].Minute)
                {
                    currentTillTheClosingTimeSlots.Remove(currentTillTheClosingTimeSlots[j]);
                }

            }
        }



        return currentTillTheClosingTimeSlots;
    }

    public async Task<BookingDto> AddBookingAsync(BookingDto bookingDto)
    {
        
        //make sure that the branch doesn't exist before
        var result = await _repo.AddBookingAsync(bookingDto);

        return result;
    }

    private List<DateTime> CalculateTimeslots(TimeSpan fromTimeSpan, TimeSpan toTimeSpan, DateTime date,
        bool excludeLastSlot = false)
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

            if (from.AddMinutes(intervals) >= to && excludeLastSlot)
                break;
        }

        return timetable;
    }
}