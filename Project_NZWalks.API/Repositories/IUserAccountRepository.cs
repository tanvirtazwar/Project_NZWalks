using Microsoft.AspNetCore.Identity;

namespace Project_NZWalks.API.Repositories;

public interface IUserAccountRepository
{
    Task<IdentityResult> UpdatePasswordAsync
        (string username, string currentPassword, string newPassword);
    Task<IdentityResult> DeleteUserAsync
        (string username, string password);
}
