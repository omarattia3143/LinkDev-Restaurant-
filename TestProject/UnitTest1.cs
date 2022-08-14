using System.Collections;
using System.Globalization;
using LinkDev.EgyptianRecipes.Data.Entities;
using Xunit.Abstractions;

namespace TestProject;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    public static readonly object[][] CalculateTimeslotsParams =
    {
        new object[] {new TimeSpan(0, 0, 0, 0), new TimeSpan(0, 23, 30, 0), DateTime.Today}
    };

    [Theory, MemberData(nameof(CalculateTimeslotsParams))]
    private List<DateTime> CalculateTimeslots(TimeSpan fromTimeSpan, TimeSpan toTimeSpan, DateTime date, bool excludeLastSlot = false)
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


    public static readonly object[][] AvailableTimeslotsFromBranchesParams =
    {
        new object[] {new DateTime(2022,8,12,21,0,0), 1}
    };


    [Theory, MemberData(nameof(AvailableTimeslotsFromBranchesParams))]
    public async Task<List<DateTime>> AvailableTimeslotsFromBranches(DateTime requestedBookingTime, int branchId)
    {
        if (requestedBookingTime < DateTime.Now)
            throw new Exception("1");

        // var timingConfiguration = await _context
        //     .Branches
        //     .FirstOrDefaultAsync(x => x.Id == branchId);

        var timingConfiguration = new Branch
        {
            Id = branchId,
            Title = "far3 el dokki",
            ManagerName = "7amada",
            OpeningTime = new TimeSpan(0, 11, 0, 0),
            ClosingTime = new TimeSpan(0, 24, 0, 0),
        };

        if (timingConfiguration == null)
            throw new Exception("2");

        //the busy slots
        // var bookingsOfTheDay = await _context.Bookings
        //     .Where(x => x.BookingStartDateTime >= requestedBookingTime)
        //     .Where(x => x.BookingEndDateTime.TimeOfDay <= timingConfiguration.ClosingTime)
        //     .ToListAsync();

        List<Booking> bookingsOfTheDay = new()
        {
            new Booking
            {
                Id = 1,
                Username = "Omar Attia",
                Phone = "012",
                BranchId = branchId,
                NumberOfChairs = 4,
                BookingStartDateTime = new DateTime(2022,8,12,22,0,0),
                BookingEndDateTime = new DateTime(2022,8,12,22,30,0)
            }
        };

        List<DateTime> busyTimeSlots = new();
        foreach (var booking in bookingsOfTheDay)
        {
            var timeslots = CalculateTimeslots(booking.BookingStartDateTime.TimeOfDay,
                booking.BookingEndDateTime.TimeOfDay, requestedBookingTime.Date,true);

            busyTimeSlots.AddRange(timeslots);
        }


        //all time slots since now till the end of the shift
        var currentTillTheClosingTimeSlots = CalculateTimeslots(requestedBookingTime.TimeOfDay,
            timingConfiguration.ClosingTime, requestedBookingTime.Date);


        IEnumerable<DateTime> common = currentTillTheClosingTimeSlots.Intersect(busyTimeSlots).ToList();

        currentTillTheClosingTimeSlots.RemoveAll(x => common.Contains(x));

        foreach (var slot in currentTillTheClosingTimeSlots)
        {
            _outputHelper.WriteLine(slot.ToString(CultureInfo.CurrentCulture));
        }


        return currentTillTheClosingTimeSlots;
    }

    [Fact]
    void TestParse()
    {
        var from = "6:30:00 AM";
        
        var TimeFrom = DateTime.ParseExact(from,"h:mm:ss tt", CultureInfo.InvariantCulture);

        _outputHelper.WriteLine(TimeFrom.ToString());
        
    }
}