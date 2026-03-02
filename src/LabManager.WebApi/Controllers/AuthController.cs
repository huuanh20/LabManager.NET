using System.Threading.Tasks;
using LabManager.Application.DTOs.Auth;
using LabManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        
        if (response == null)
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(response);
    }
}
