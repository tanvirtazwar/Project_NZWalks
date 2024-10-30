using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Data;
using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext;

        public SQLWalkRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync
            (
            string? filterOn = null, string? filterQuery = null,
            bool filterOnLength = false, double? filterDistanceUpper = null,
            double? filterDistanceLower = null, 
            string? sortBy = null, bool isAscending = true, 
            int pageNumber = 1, int pageSize = 1000
            )
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region");

            //Filtering

            if (string.IsNullOrWhiteSpace(filterOn) == false &&
                string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
            }
            if (filterOnLength)
            {
                if (filterDistanceUpper is not null && filterDistanceLower is not null 
                    && filterDistanceUpper >= filterDistanceLower && filterDistanceLower >= 0)
                {
                    walks = walks.Where(x => x.LengthInKm <= filterDistanceUpper 
                    && x.LengthInKm >= filterDistanceLower);
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x => x.Name):
                        walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Description) :
                        walks.OrderByDescending(x => x.Description);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) :
                        walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults =(pageNumber - 1)*pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
}
