using LinkDev.EgyptianRecipes.Data;
using LinkDev.EgyptianRecipes.Data.Dtos;
using LinkDev.EgyptianRecipes.Data.Entities;
using LinkDev.EgyptianRecipes.Helper;
using LinkDev.EgyptianRecipes.Pagination;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.EgyptianRecipes.Repositories;

public class BranchRepo : IBranchRepo
{
    private readonly RestaurantContext _context;

    public BranchRepo(RestaurantContext context)
    {
        _context = context;
    }

    public async Task<PagedList<BranchDto>> GetAllBranchesAsync(PaginationParams paginationParams)
    {
        var query = _context.Branches.AsQueryable();

        //query search by title
        if(!string.IsNullOrEmpty(paginationParams.Title)) 
            query = query.Where(x => x.Title.Contains(paginationParams.Title));

        return await PagedList<BranchDto>.CreateAsync(query.ProjectToType<BranchDto>().AsNoTracking(),
            paginationParams.PageNumber, paginationParams.PageSize);
    }

    public async Task<BranchDto> GetBranchesByIdAsync(int id)
    {
        return await _context.Branches.ProjectToType<BranchDto>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BranchDto> GetBranchesByNameAsync(string branchName)
    {
        return await _context.Branches.ProjectToType<BranchDto>().FirstOrDefaultAsync(x => x.Title == branchName);
    }


    public async Task<BranchDto> AddBranchAsync(BranchDto branchDto)
    {
        var branch = branchDto.Adapt<Branch>();

        await _context.Branches.AddAsync(branch);
        var result = await _context.SaveChangesAsync();

        if (result <= 0)
            return null;

        return branchDto;
    }

    public async Task<bool> DeleteBranchAsync(int id)
    {
        _context.Branches.Remove(new Branch {Id = id});
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}