using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_NZWalks.API.Controllers
{
    
    //https://localhost:7192/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: https://localhost:7192/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsName = 
                ["John", "Jane", "Mark"];
            return Ok(studentsName);
        }
    }
}
