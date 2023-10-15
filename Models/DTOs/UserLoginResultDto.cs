using System.ComponentModel.DataAnnotations;

namespace JWTauthentification.Api.Models.DTOs;

public class UserLoginResultDto
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}