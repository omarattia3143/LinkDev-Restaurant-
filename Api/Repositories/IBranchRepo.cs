using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;

namespace LinkDev.EgyptianRecipes.Repositories;

public interface IBranchRepo
{
    Task<IEnumerable<BranchDto>> GetAllBranchesAsync();
    Task<BranchDto> GetBranchesByIdAsync(int id);
    Task<BranchDto> GetBranchesByNameAsync(string branchName);
    Task<BranchDto> AddBranchAsync(BranchDto branchDto);
    Task<bool> DeleteBranchAsync(int id);
}