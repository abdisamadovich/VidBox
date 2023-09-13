namespace VidBox.Service.Dtos.Videos;

public class VideoCreateDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoPath { get; set; } = string.Empty;
}
