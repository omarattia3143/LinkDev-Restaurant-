using LinkDev.EgyptianRecipes.Data.Entities;

namespace LinkDev.EgyptianRecipes.Repositories;

public interface IBranchRepo
{
    Task<IEnumerable<Branch>> GetAllBranchesAsync();
    Task<Branch> GetBranchesByIdAsync(int id);
    Task<Branch> GetBranchesByNameAsync(string branchName);
    Task<Branch> AddBranchAsync(Branch branch);
    Task<bool> DeleteBranchAsync(int id);
}