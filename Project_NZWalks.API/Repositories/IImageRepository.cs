using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
