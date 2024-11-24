using Microsoft.AspNetCore.Identity;

namespace Project_NZWalks.API.Repositories;

public class UserAccountRepository
    (UserManager<IdentityUser> userManager) 
    : IUserAccountRepository
{
    public async Task<IdentityResult> UpdatePasswordAsync
        (string username, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(username);
        if (user == null)

        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        var checkPasswordResult = await userManager.CheckPasswordAsync(user, currentPassword);
        if (!checkPasswordResult)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Incorrect password" });
        }

        return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<IdentityResult> DeleteUserAsync(string username, string password)
    {
        var user = await userManager.FindByEmailAsync(username);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        var checkPasswordResult = await userManager.CheckPasswordAsync(user, password);
        if (!checkPasswordResult)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Incorrect password" });
        }

        return await userManager.DeleteAsync(user);
    }
}
