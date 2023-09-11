using Microsoft.AspNetCore.Http;
using VidBox.Service.Interfaces.Common;

namespace VidBox.Service.Services.Commons;

public class FileService : IFileService
{
    public Task<bool> DeleteVideoAsync(string subpath)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadVideoAsync(IFormFile image)
    {
        throw new NotImplementedException();
    }
}
