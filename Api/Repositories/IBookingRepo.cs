using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;

namespace LinkDev.EgyptianRecipes.Repositories;

public interface IBookingRepo
{
    Task<BookingDto> AddBookingAsync(BookingDto bookingDto);
    Task<BranchShift> GetBranchShift(int id);
    Task<IEnumerable<Booking>> GetBusyTimeslots(DateTime requestedBookingTime, TimeSpan closingTime);
}