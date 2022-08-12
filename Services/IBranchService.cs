using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;

namespace LinkDev.EgyptianRecipes.Services;

public interface IBranchService
{
    /// <summary>
    /// List all branches
    /// </summary>
    /// <returns>IEnumerable of Branches</returns>
    Task<IEnumerable<Branch>> GetAllBranchesAsync();

    /// <summary>
    /// Add Branch with unique name
    /// </summary>
    /// <param name="branchDto"></param>
    /// <returns>Object of the newly created Branch</returns>
    Task<Branch> AddBranchAsync(BranchDto branchDto);

    /// <summary>
    /// Delete branch by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteBranch(int id);
}