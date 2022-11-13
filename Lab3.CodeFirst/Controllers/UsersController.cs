using Lab3.CodeFirst.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.CodeFirst.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ScheduleDbContext _context;

        public UsersController(ScheduleDbContext context)
        {
            _context = context;
        }
         
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.AsQueryable());
        }
    }
}
