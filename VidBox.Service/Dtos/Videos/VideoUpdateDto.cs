using Microsoft.AspNetCore.Http;

namespace VidBox.Service.Dtos.Videos;

public class VideoUpdateDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
