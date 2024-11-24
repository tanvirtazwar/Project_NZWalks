using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Models.User;
using Project_NZWalks.API.Repositories;

namespace Project_NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController
    (UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    ITokenRepository tokenRepository,
    IUserAccountRepository userAccountRepository)
    : ControllerBase
{
    //Post :api/Auth/Register
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = new AppUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
            };

            var identityResult =
                await userManager.CreateAsync(appUser, registerRequestDto.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest("Something went wrong");
            }
            //Add role to the user
            if (string.IsNullOrEmpty(registerRequestDto.Role))
            {
                return BadRequest("Something went wrong");
            }

            identityResult =
                await userManager.AddToRoleAsync(appUser, registerRequestDto.Role);

            if (!identityResult.Succeeded)
            {
                return BadRequest("Something went wrong");
            }

            return Ok("User was Registered. Please login.");
        }
        catch (Exception e)
        {

            return StatusCode(500, e.Message);
        }
    }

    //Post :api/Auth/Login
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(appUser =>
            appUser.Email == loginRequestDto.Email);

        if (user == null)
        {
            return BadRequest("UserEmail or Password incorrect");
        }

        var passwordResult = await signInManager
            .CheckPasswordSignInAsync(user, loginRequestDto.Password, false);

        if (!passwordResult.Succeeded)
        {
            return BadRequest("UserEmail or Password incorrect");
        }

        //Get Roles
        var roles = await userManager.GetRolesAsync(user);
        if (roles == null!)
        {
            return BadRequest("UserEmail or Password incorrect");
        }

        //Create Token
        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
        var loginResponse = new LoginResponseDto
        {
            JwtToken = jwtToken
        };
        return Ok(loginResponse);

    }

    // POST: api/Auth/UpdatePassword
    [HttpPost]
    [Route("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword
        ([FromBody] UpdatePasswordRequestDto updatePasswordRequestDto)
    {
        try
        {
            var identityResult = await userAccountRepository.UpdatePasswordAsync(
                updatePasswordRequestDto.Email,
                updatePasswordRequestDto.CurrentPassword,
                updatePasswordRequestDto.NewPassword);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors.FirstOrDefault()?.Description);
            }

            return Ok("Password Updated Successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("DeleteUser")]
    public async Task<IActionResult> DeleteUser
        ([FromBody] DeletePasswordRequestDto deletePasswordRequest)
    {
        try
        {
            var identityResult = await userAccountRepository.DeleteUserAsync
                (deletePasswordRequest.Email, deletePasswordRequest.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors.FirstOrDefault()?.Description);
            }

            return Ok("User Deleted Successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
