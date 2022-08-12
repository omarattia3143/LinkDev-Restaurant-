using LinkDev.EgyptianRecipes.Data.Dtos;

namespace LinkDev.EgyptianRecipes.Repositories;

public interface IBookingRepo
{
    Task<BookingDto> AddBookingAsync(BookingDto bookingDto);
    Task<BranchShift> GetBranchShift(int id);
    Task<IEnumerable<BookingDto>> GetBusyTimeslots(DateTime requestedBookingTime, TimeSpan closingTime);
}