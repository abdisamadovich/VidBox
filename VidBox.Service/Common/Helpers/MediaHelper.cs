namespace VidBox.Service.Common.Helpers;

public class MediaHelper
{
    public static string MakeVideoName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "VID_" + Guid.NewGuid() + extension;
        return name;
    }

    public static string[] GetVideoExtensions()
    {
        return new string[]
        {
            // MP$ files
            ".mp4",
        };
    }
}
