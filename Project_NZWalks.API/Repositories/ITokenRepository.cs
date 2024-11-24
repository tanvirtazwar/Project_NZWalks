using Microsoft.AspNetCore.Identity;
using Project_NZWalks.API.Models.User;

namespace Project_NZWalks.API.Repositories;

public interface ITokenRepository
{
    string CreateJWTToken(AppUser user, List<string> roles);
}
