using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Data;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Querying;

namespace Project_NZWalks.API.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext dbContext;

    public SQLWalkRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public Task<List<Walk>> GetAllAsync(QueryWalks query)
    {
        var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

        walks = string.IsNullOrEmpty(query.WalksName)? walks 
            : walks.Where(walk => walk.Name.Contains(query.WalksName));
        walks = string.IsNullOrEmpty(query.RegionName) ? walks
            : walks.Where(walk => walk.Region.Name.Contains(query.RegionName));
        walks = string.IsNullOrEmpty(query.DifficulryLevel) ? walks
            : walks.Where(walk => walk.Difficulty.Name.Contains(query.DifficulryLevel));

        if (query.SortByDistance)
        {
            walks = query.IsDescending? walks.OrderByDescending(walk => walk.LengthInKm) 
                : walks.OrderBy(walk => walk.LengthInKm);
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return walks.Skip(skipNumber)
                .Take(query.PageSize).ToListAsync();
    }

    public async Task<Walk?> GetByIDAsync(Guid id)
    {
        return await dbContext.Walks.Include("Difficulty").
            Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk != null)
        {
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
        }

        return await dbContext.Walks.Include("Difficulty").
            Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = await dbContext.Walks.Include("Difficulty").
            Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk != null)
        {
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
        }

        return existingWalk;
    }
}
