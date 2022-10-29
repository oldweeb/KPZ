using AutoMapper;
using Lab3.CodeFirst.DB;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public UsersController(ScheduleDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(Guid id)
        {
            User? user = await _context.FindAsync<User>(id);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Set<User>().AsEnumerable());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            if (user.Position == Position.Student && user.GroupId is null)
            {
                return BadRequest("GroupId must be included for user of type student.");
            }

            if (await _context.Set<User>().AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest($"User with email {user.Email} already exists.");
            }

            var @new = _mapper.Map<User>(user);
            @new.Email = @new.Email.ToLower();

            if (user.Position is not Position.Student)
            {
                await _context.AddAsync(@new);
                await _context.SaveChangesAsync();
                return Accepted();
            }
            
            Group? group = await _context.FindAsync<Group>(user.GroupId);
            if (group is null)
            {
                return BadRequest($"Group with id {user.GroupId} does not exist.");
            }

            @new.Group = group;
            await _context.AddAsync(@new);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            if (user.Id is null)
            {
                return BadRequest("Id property is missing.");
            }

            user.Email = user.Email.ToLower();
            if (await _context.Set<User>().AnyAsync(u => u.Email == user.Email && u.Id != user.Id))
            {
                return BadRequest($"User with email {user.Email} already exists.");
            }

            if (user.Position is Position.Student && user.GroupId is null)
            {
                return BadRequest("GroupId must be included for user of type student.");
            }

            var @new = _mapper.Map<User>(user);
            if (user.Position is not Position.Student)
            {
                _context.Entry(@new).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }

            Group? group = await _context.FindAsync<Group>(user.GroupId);
            if (group is null)
            {
                return BadRequest($"Group with id {user.GroupId} does not exist.");
            }

            @new.Group = group;
            _context.Entry(@new).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            User? user = await _context.FindAsync<User>(id);
            if (user is null)
            {
                return NotFound($"User with id {id} does not exist.");
            }

            _context.Entry(user).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
