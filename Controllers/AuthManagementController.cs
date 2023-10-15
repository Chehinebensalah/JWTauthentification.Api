using JWTauthentification.Api.Configurations;
using JWTauthentification.Api.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JWTauthentification.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthManagementController :ControllerBase
{

    private readonly ILogger<AuthManagementController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;
    
    public AuthManagementController(
        ILogger<AuthManagementController> logger, 
        UserManager<IdentityUser> userManager, 
        OptionsMonitor<JwtConfig> _optionsMonitor)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtConfig = _optionsMonitor.CurrentValue;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
    {
        if (ModelState.IsValid)
        {
            var emailExist = await _userManager.FindByEmailAsync(requestDto.Email);
            if (emailExist != null)
            {
                return BadRequest("email already exist ");
            }
            var newUser = new IdentityUser()
            {
                Email = requestDto.Email
            };
            var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);
            if (isCreated.Succeeded)
            {
                //Generate Token
                return Ok(new RegistrationRequestResponse()
                {
                    Result = true,
                    Token = "",
                });
            }

            return BadRequest("Error Creating User try again !");
        }
        
        return BadRequest("Invalid request payload");
    }

    
}
