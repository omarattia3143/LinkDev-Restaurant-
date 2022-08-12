using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.EgyptianRecipes.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
// [Authorize(Roles = "admin")]
public class BookingController : Controller
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> AvailableTimeslotsFromBranches(int id)
    {
        var requestedBookingTime = DateTime.Now;
        var result = await _service.AvailableTimeslotsFromBranches(requestedBookingTime, id);

        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBooking(BookingDto bookingDto)
    {
        if (!ModelState.IsValid)
            return Json("model is not valid");

        var result = await _service.AddBookingAsync(bookingDto);

        return Json(result);
    }
}