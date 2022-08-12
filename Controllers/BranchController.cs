using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.EgyptianRecipes.Controllers;

[Authorize(Roles = "admin")]
public class BranchController : Controller
{
    private readonly IBranchService _service;

    public BranchController(IBranchService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAllBranchesAsync();

        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(BranchDto branchDto)
    {
        if (!ModelState.IsValid)
            return Json("model is not valid");
            
        var result = await _service.AddBranchAsync(branchDto);

        return Json(result);
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteBranch(id);

        return Json(result);
    }
}