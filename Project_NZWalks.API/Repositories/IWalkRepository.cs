using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Querying;

namespace Project_NZWalks.API.Repositories;

public interface IWalkRepository
{
    Task<Walk?> CreateAsync(Walk walk);

    Task<List<Walk>> GetAllAsync (QueryWalks query);

    Task<Walk?> GetByIDAsync(Guid id);

    Task<Walk?> UpdateAsync(Guid id, Walk walk);

    Task<Walk?> DeleteAsync(Guid id);
}
