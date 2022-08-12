using LinkDev.EgyptianRecipes.Data;
using LinkDev.EgyptianRecipes.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.EgyptianRecipes.Repositories;

public class BranchRepo : IBranchRepo
{
    private readonly RestaurantContext _context;

    public BranchRepo(RestaurantContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
    {
        return await _context.Branches.ToListAsync();
    }

    public async Task<Branch> GetBranchesByIdAsync(int id)
    {
        return await _context.Branches.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Branch> GetBranchesByNameAsync(string branchName)
    {
        return await _context.Branches.FirstOrDefaultAsync(x => x.Title == branchName);
    }



    public async Task<Branch> AddBranchAsync(Branch branch)
    {
        await _context.Branches.AddAsync(branch);
        var result = await _context.SaveChangesAsync();

        if (result <= 0)
            return null;

        return branch;
    }

    public async Task<bool> DeleteBranchAsync(int id)
    {
        _context.Branches.Remove(new Branch {Id = id});
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}