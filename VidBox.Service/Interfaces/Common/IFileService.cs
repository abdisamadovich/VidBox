using Microsoft.AspNetCore.Http;

namespace VidBox.Service.Interfaces.Common;

public interface IFileService
{
    // returns sub path of this video
    public Task<string> UploadVideoAsync(IFormFile video);
    public Task<bool> DeleteVideoAsync(string subpath);
}
