using System.ComponentModel.DataAnnotations;

namespace VidBox.Domain.Entities.Adminstrators
{
    public class Adminstrator : Auditable
    {
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
        public string PasswordHash { get; set; } = String.Empty;
        public string Salt { get; set; } = String.Empty;
    }
}
