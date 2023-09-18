using VidBox.Service.Services.Models;

namespace VidBox.Service.Services;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3Obj,AwsCredentials awsCredentials);
}
