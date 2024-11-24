using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;

namespace Project_NZWalks.API.Repositories;

public interface IImageRepository
{
    Task<Image> UploadAsync(Image image);
    Task<Image?> GetByIdAsync(Guid id);
    Task<IEnumerable<Image>> GetAllAsync();
    Task<Image?> UpdateAsync(Guid id, ImageUpdateRequestDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
