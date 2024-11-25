using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Data;

namespace Project_NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController (NZWalksDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Difficulties.ToListAsync());
        }
    }
}
