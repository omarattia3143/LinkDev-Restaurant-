using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;
using LinkDev.EgyptianRecipes.Helper;
using LinkDev.EgyptianRecipes.Pagination;

namespace LinkDev.EgyptianRecipes.Repositories;

public interface IBranchRepo
{
    Task<PagedList<BranchDto>> GetAllBranchesAsync(PaginationParams paginationParams); 
    Task<BranchDto> GetBranchesByIdAsync(int id);
    Task<BranchDto> GetBranchesByNameAsync(string branchName);
    Task<BranchDto> AddBranchAsync(BranchDto branchDto);
    Task<bool> DeleteBranchAsync(int id);
}