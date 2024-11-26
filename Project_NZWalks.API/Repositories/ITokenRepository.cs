using Project_NZWalks.API.Models.User;

namespace Project_NZWalks.API.Repositories;

public interface ITokenRepository
{
    string CreateJwtToken(AppUser user, List<string> roles);
}
