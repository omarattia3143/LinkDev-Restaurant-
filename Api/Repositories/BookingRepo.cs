using System.Globalization;
using LinkDev.EgyptianRecipes.Data;
using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.EgyptianRecipes.Repositories;

public class BookingRepo : IBookingRepo
{
    private readonly RestaurantContext _restaurantContext;
    private readonly IBranchRepo _branchRepo;

    public BookingRepo(RestaurantContext restaurantContext, IBranchRepo branchRepo)
    {
        _restaurantContext = restaurantContext;
        _branchRepo = branchRepo;
    }
    
    
    public async Task<BookingDto> AddBookingAsync(BookingDto bookingDto)
    {
        var booking = bookingDto.Adapt<Booking>();

        
        var dateTimeFrom = DateTime.ParseExact(bookingDto.TimeslotsFrom,"h:mm:ss tt", CultureInfo.InvariantCulture);
        var dateTimeTo = DateTime.ParseExact(bookingDto.TimeslotsTo,"h:mm:ss tt", CultureInfo.InvariantCulture);

        booking.BookingStartDateTime = new DateTime(bookingDto.PickedDate.Year, bookingDto.PickedDate.Month, bookingDto.PickedDate.Day,
            dateTimeFrom.Hour, dateTimeFrom.Minute, 0);
        
        booking.BookingEndDateTime = new DateTime(bookingDto.PickedDate.Year, bookingDto.PickedDate.Month, bookingDto.PickedDate.Day,
            dateTimeTo.Hour, dateTimeTo.Minute, 0);


        await _restaurantContext.Bookings.AddAsync(booking);
        var result = await _restaurantContext.SaveChangesAsync();

        if (result <= 0)
            return null;

        return bookingDto;
    }

    public async Task<BranchShift> GetBranchShift(int id)
    {
        var branch = await _branchRepo.GetBranchesByIdAsync(id);

        return branch.Adapt<BranchShift>();
    }
    
    public async Task<IEnumerable<Booking>> GetBusyTimeslots(DateTime requestedBookingTime, TimeSpan closingTime)
    {
       var bookingsOfTheDay = await _restaurantContext.Bookings
            .Where(x => x.BookingStartDateTime >= requestedBookingTime)
            .Where(x => x.BookingEndDateTime.TimeOfDay <= closingTime)
            .ToListAsync();

       return bookingsOfTheDay;
    }
    
    

}