﻿namespace VidBox.Service.Dtos.Auth;

public class LoginDto
{
    public string Name { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = string.Empty;
}
