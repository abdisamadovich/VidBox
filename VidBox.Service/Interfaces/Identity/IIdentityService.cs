namespace VidBox.Service.Interfaces.Identity;

public interface IIdentityService
{
    public long UserId { get; }
    public string Name { get; }
    public string PhoneNumber { get; }
    public string IdentityRole { get; }
}
