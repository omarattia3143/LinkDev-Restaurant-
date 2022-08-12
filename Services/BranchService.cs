using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;
using LinkDev.EgyptianRecipes.Repositories;
using Mapster;

namespace LinkDev.EgyptianRecipes.Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepo _repo;

    public BranchService(IBranchRepo repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// List all branches
    /// </summary>
    /// <returns>IEnumerable of Branches</returns>
    public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
    {
        var result = await GetAllBranchesAsync();

        return result;
    }

    /// <summary>
    /// Add Branch with unique name
    /// </summary>
    /// <param name="branchDto"></param>
    /// <returns>Object of the newly created Branch</returns>
    public async Task<Branch> AddBranchAsync(BranchDto branchDto)
    {
        //mapping from dto to domain model
        var branch = branchDto.Adapt<Branch>();
        
        //make sure that the branch doesn't exist before
        var existingBranch = await _repo.GetBranchesByNameAsync(branch.Title.ToLower().Trim());

        if (existingBranch is not null)
            throw new BadHttpRequestException("Branch Already Exists");

        return await _repo.AddBranchAsync(branch);
    }

    /// <summary>
    /// Delete branch by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteBranch(int id)
    {
        var existingBranch = await _repo.GetBranchesByIdAsync(id);

        if (existingBranch is null)
            throw new BadHttpRequestException("Branch Doesn't exist");
        
        return await _repo.DeleteBranchAsync(id);
    }
    
}