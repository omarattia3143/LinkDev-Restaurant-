using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Extensions;
using LinkDev.EgyptianRecipes.Pagination;
using LinkDev.EgyptianRecipes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.EgyptianRecipes.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
// [Authorize(Roles = "admin")]
public class BranchController : Controller
{
    private readonly IBranchService _service;

    public BranchController(IBranchService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllBranches([FromQuery] PaginationParams paginationParams)
    {
        var result = await _service.GetAllBranchesAsync(paginationParams);
        
        Response.AddPaginationHeader(result.CurrentPage,result.PageSize, result.TotalCount,result.TotalPages);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBranch(BranchDto branchDto)
    {
        if (!ModelState.IsValid)
            return Json("model is not valid");
            
        var result = await _service.AddBranchAsync(branchDto);

        return Ok(result);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteBranch(int id)
    {
        var result = await _service.DeleteBranch(id);

        return Ok(result);
    }
}