using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;
using LinkDev.EgyptianRecipes.Helper;
using LinkDev.EgyptianRecipes.Pagination;

namespace LinkDev.EgyptianRecipes.Services;

public interface IBranchService
{
    /// <summary>
    /// List all branches
    /// </summary>
    /// <returns>IEnumerable of Branches</returns>
    Task<PagedList<BranchDto>> GetAllBranchesAsync(PaginationParams paginationParams);

    /// <summary>
    /// Add Branch with unique name
    /// </summary>
    /// <param name="branchDto"></param>
    /// <returns>Object of the newly created Branch</returns>
    Task<BranchDto> AddBranchAsync(BranchDto branchDto);

    /// <summary>
    /// Delete branch by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteBranch(int id);
}