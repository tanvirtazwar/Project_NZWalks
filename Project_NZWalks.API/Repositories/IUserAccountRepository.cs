using Microsoft.AspNetCore.Identity;

namespace Project_NZWalks.API.Repositories;

public interface IUserAccountRepository
{
    Task<IdentityResult> UpdatePasswordAsync
        (string email, string currentPassword, string newPassword);
    Task<IdentityResult> DeleteUserAsync
        (string email, string password);

}
