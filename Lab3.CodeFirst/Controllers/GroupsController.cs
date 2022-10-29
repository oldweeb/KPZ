using AutoMapper;
using Lab3.CodeFirst.DB;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab3.CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public GroupsController(ScheduleDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            Group? group = await _context.FindAsync<Group>(id);

            if (group is null)
            {
                return NotFound($"Group with id {id} does not exist.");
            }

            return Ok(group);
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            var groups = _context.Set<Group>();
            return Ok(groups.AsEnumerable());
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] GroupDto group)
        {
            if (await _context.Set<Group>().FirstOrDefaultAsync(g => group.Name == g.Name) is not null)
            {
                return BadRequest($"Group with Name = {group.Name} already exists.");
            }

            Group @new = _mapper.Map<Group>(group);
            await _context.AddAsync(@new);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody, JsonProperty("group")] GroupDto groupDto)
        {
            if (groupDto.Id is null)
            {
                return BadRequest("Request body must include id property.");
            }

            Group? group = await _context.FindAsync<Group>(groupDto.Id);
            if (group is null)
            {
                return NotFound($"Group with id {groupDto.Id} does not exist.");
            }

            if (await _context.Set<Group>().AnyAsync(g => g.Id != groupDto.Id && g.Name.ToLower() == groupDto.Name.ToLower()))
            {
                return BadRequest($"There is already group with Name = {groupDto.Name}");
            }

            group.Name = groupDto.Name;
            _context.Entry(group);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Route("{id}/students")]
        [HttpGet]
        public async Task<IActionResult> GetGroupStudents(Guid id)
        {
            Group? group = await _context.FindAsync<Group>(id);
            if (group is null)
            {
                return NotFound($"Group with id {id} does not exist.");
            }

            return Ok(group.Students);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            Group? group = await _context.FindAsync<Group>(id);
            if (group is null)
            {
                return NotFound($"Group with id {id} does not exist.");
            }

            return Ok();
        }
    }
}
