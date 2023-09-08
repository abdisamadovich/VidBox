using VidBox.Domain.Entities;

namespace VidBox.DataAccess.ViewModels.Users
{
    public class UserViewModel : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
    }
}
