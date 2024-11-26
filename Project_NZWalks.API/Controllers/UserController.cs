using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Repositories;
using System.Security.Claims;

namespace Project_NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IUserAccountRepository userAccountRepository) : ControllerBase
    {
        // POST: api/User/UpdatePassword
        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        // POST: api/User/DeleteUser
        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeletePasswordRequestDto deletePasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var identityResult = await userAccountRepository.DeleteUserAsync(
                    deletePasswordRequest.Email,
                    deletePasswordRequest.Password);

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

        // GET: api/User/GetUserInformation
        [HttpGet]
        [Route("GetUserInformation")]
        public IActionResult GetUserInformation()
        {
            try
            {
                // Retrieve user claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = User.Identity?.Name;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not logged in or token is invalid.");
                }

                return Ok(new
                {
                    UserId = userId,
                    Name = userName,
                    Email = email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
