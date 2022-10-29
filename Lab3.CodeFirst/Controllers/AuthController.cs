using System.Security.Claims;
using Lab3.CodeFirst.DTOs;
using Lab3.CodeFirst.Models;
using Lab3.CodeFirst.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.CodeFirst.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private const string PositionClaimType = "position";
    private readonly ILoginService _service;

    public AuthController(ILoginService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        try
        {
            string token = await _service.Login(login);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto data)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);

        if (!_service.ValidateWriteRights((Position)Int32.Parse(positionClaim.Value)))
        {
            return Forbid();
        }

        try
        {
            await _service.CreateUser(data);
            return Accepted();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto data)
    {
        Claim positionClaim = HttpContext.User.Claims.First(c => c.Type == PositionClaimType);

        if (!_service.ValidateWriteRights((Position)Int32.Parse(positionClaim.Value)))
        {
            return Forbid();
        }

        try
        {
            await _service.UpdateUser(data);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}