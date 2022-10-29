using System.Security.Claims;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Lab3.CodeFirst.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.CodeFirst.Controllers;

[Route("[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private const string PositionClaimType = "position";
    private readonly IGroupService _service;

    public GroupsController(IGroupService groupService)
    {
        _service = groupService;
    }

    [Authorize]
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetGroupById(Guid id)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        Group? group = await _service.GetGroupById(id);

        if (group is null)
        {
            return NotFound($"Group with id {id} does not exist.");
        }

        return Ok(group);
    }

    [Authorize]
    [Route("all")]
    [HttpGet]
    public IActionResult GetAllGroups()
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        var groups = _service.GetGroups();
        return Ok(groups);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddGroup([FromBody] GroupDto group)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        try
        {
            await _service.AddGroup(group);
            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateGroup([FromBody] GroupDto group)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        try
        {
            await _service.UpdateGroup(group);
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
    public async Task<IActionResult> DeleteGroup(Guid id)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);
        var position = (Position)Int32.Parse(positionClaim.Value);
        if (!_service.ValidateWriteRights(position))
        {
            return Forbid();
        }

        bool result = await _service.DeleteGroup(id);
        return result ? Ok() : NotFound($"Group with id {id} does not exist.");
    }
}