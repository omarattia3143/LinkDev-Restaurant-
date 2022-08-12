using LinkDev.EgyptianRecipes.Data.Dtos;

namespace LinkDev.EgyptianRecipes.Services;

public interface IBookingService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestedBookingTime">current of user's datetime to start looking for slots after datetime.now</param>
    /// <param name="branchId">branch Id that the user is willing to make a booking in</param>
    /// <returns>the available timeslots</returns>
    /// <exception cref="Exception"></exception>
    Task<List<DateTime>> AvailableTimeslotsFromBranches(DateTime requestedBookingTime, int branchId);

    Task<BookingDto> AddBookingAsync(BookingDto bookingDto);
}