using AutoMapper;
using Lab3.DbFirst.DTOs;
using Lab3.DbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab3.DbFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public EventsController(Lab3DbFirstContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        Event? @event = await _context.FindAsync<Event>(id);
        if (@event is null)
        {
            return NotFound();
        }

        return Ok(@event);
    }

    [Route("all")]
    [HttpGet]
    public IActionResult GetAllEvents()
    {
        var events = _context.Set<Event>();
        return Ok(events.AsEnumerable());
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody, JsonProperty("event")] EventDto eventDto)
    {
        var @event = _mapper.Map<Event>(eventDto);

        User? user = await _context.FindAsync<User>(eventDto.ProfessorId);

        if (user is null)
        {
            return BadRequest($"User with id {eventDto.ProfessorId} does not exist.");
        }

        if (user.Position is Position.Student or Position.SystemAdministrator)
        {
            return BadRequest($"Requested user is not of professor type.");
        }

        Group? group = await _context.FindAsync<Group>(eventDto.GroupId);
        if (group is null)
        {
            return BadRequest($"Group with id {eventDto.GroupId} does not exist.");
        }

        @event.Professor = user;
        @event.Group = group;

        try
        {
            await _context.AddAsync(@event);
            await _context.SaveChangesAsync();

            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEvent([FromBody] EventDto eventDto)
    {
        if (eventDto.Id is null)
        {
            return BadRequest("EventId must be included in the request.");
        }

        var @event = _mapper.Map<Event>(eventDto);

        User? user = await _context.FindAsync<User>(eventDto.ProfessorId);

        if (user is null)
        {
            return BadRequest($"User with id {eventDto.ProfessorId} does not exist.");
        }

        if (user.Position is Position.Student or Position.SystemAdministrator)
        {
            return BadRequest("Requested user is not of professor type.");
        }

        Group? group = await _context.FindAsync<Group>(eventDto.GroupId);
        if (group is null)
        {
            return BadRequest($"Group with id {eventDto.GroupId} does not exist.");
        }

        @event.Professor = user;
        @event.Group = group;

        try
        {
            _context.Entry(@event).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Route("{id}/students")]
    [HttpGet]
    public async Task<IActionResult> GetStudents(Guid id)
    {
        Event? @event = await _context.FindAsync<Event>(id);
        if (@event is null)
        {
            return NotFound($"Event with id {id} does not exist.");
        }

        return Ok(@event.Group.Students);
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        Event? @event = await _context.FindAsync<Event>(id);
        if (@event is null)
        {
            return NotFound($"Event with id {id} does not exist.");
        }

        _context.Entry(@event).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

        return Ok();
    }
}