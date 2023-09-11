namespace VidBox.Service.Dtos.Auth;

public class RegisterDto
{
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = string.Empty;
}