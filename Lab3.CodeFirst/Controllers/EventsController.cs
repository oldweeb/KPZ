using System.Security.Claims;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Lab3.CodeFirst.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lab3.CodeFirst.Controllers;

[Route("[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private const string PositionClaimType = "position";
    private const string IdClaimType = "user-id";
    private readonly IEventService _service;

    public EventsController(IEventService service)
    {
        _service = service;
    }

    [Authorize]
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        Event? @event = await _service.GetEventById(id);
        if (@event is null)
        {
            return NotFound($"Event with id {id} does not exist.");
        }

        return Ok(@event);
    }

    [Authorize]
    [Route("all")]
    [HttpGet]
    public IActionResult GetAllEvents()
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        return Ok(_service.GetEvents());
    }

    [Authorize]
    [Route("my")]
    [HttpGet]
    public IActionResult GetMyEvents()
    {
        Claim idClaim = HttpContext.User.Claims.First(c => c.Type == IdClaimType);
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var id = Guid.Parse(idClaim.Value);
        var position = (Position)Int32.Parse(positionClaim.Value);

        try
        {
            IQueryable<Event> events = _service.GetEventsForUser(id, position);
            return Ok(events);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody, JsonProperty("event")] EventDto eventDto)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        try
        {
            await _service.AddEvent(eventDto);
            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateEvent([FromBody, JsonProperty("event")] EventDto eventDto)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        try
        {
            await _service.UpdateEvent(eventDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize]
    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        bool result = await _service.DeleteEvent(id);
        return result ? Ok() : NotFound($"Event with id {id} does not exist.");
    }
}