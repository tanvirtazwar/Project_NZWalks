using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync
            (string? filterOn = null, string? filterQuery = null,
            bool filterOnLength = false,double? filterDistanceUpper = null,
            double? filterDistanceLower = null,string? sortBy = null,
            bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk?> GetByIDAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
