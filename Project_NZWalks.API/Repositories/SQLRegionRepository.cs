using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Data;
using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Repositories;

public class SqlRegionRepository
    (NzWalksDbContext dbContext,
        IHttpContextAccessor httpContextAccessor) 
    : IRegionRepository
{
    public async Task<Region> CreateAsync(Region region)
    {
        region.UserId = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await dbContext.Regions.AddAsync(region);
        await dbContext.SaveChangesAsync();
        return region;
    }


    public async Task<List<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        return await dbContext.Regions.FindAsync(id);
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion == null)
        {
            return null;
        }
        if (existingRegion.UserId != httpContextAccessor.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await dbContext.SaveChangesAsync();

        return existingRegion;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion == null)
        {
            return null;
        }
        if (existingRegion.UserId != httpContextAccessor.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return null;
        }

        dbContext.Regions.Remove(existingRegion);
        await dbContext.SaveChangesAsync();

        return existingRegion;
    }
}
