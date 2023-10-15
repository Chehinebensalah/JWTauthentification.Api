namespace JWTauthentification.Api.Models;

public class AuthResult
{
    public string Token { get; set; }=String.Empty;
    public Boolean Result { get; set; }
    public List<string> Errors { get; set; }
}