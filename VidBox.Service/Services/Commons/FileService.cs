using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using VidBox.Service.Common.Helpers;
using VidBox.Service.Interfaces.Common;
using static System.Net.Mime.MediaTypeNames;


namespace VidBox.Service.Services.Commons;

public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    //private readonly string STORAGE = "storage";
    private readonly string VIDEOS = "videos";

    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env) 
    {
        ROOTPATH = env.WebRootPath;
    }
    public async Task<bool> DeleteVideoAsync(string subpath)
    {
        if (subpath == "") return true;
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else return false;
    }

    public async Task<string> UploadVideoAsync(IFormFile video)
    {
        string newVideoName = MediaHelper.MakeVideoName(video.FileName);
        string subpath = Path.Combine(MEDIA, VIDEOS, newVideoName); 
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await video.CopyToAsync(stream);
        stream.Close();

        return subpath;
    }
}
