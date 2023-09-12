using VidBox.Domain.Entities;

namespace VidBox.DataAccess.ViewModels.Adminstrator;

public class AdminstratorViewModel : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Phone_number { get; set; } = string.Empty;
    public bool Phone_number_confirmed { get; set; }
}
