using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using VidBox.Service.Services;
using VidBox.Service.Services.Models;

namespace VidBox.WebApi.Controllers.Adminstrator.Videos;

[Route("api/admin")]
[ApiController]
public class AdminVideoController : ControllerBase
{
    /*public string BucketName = "vidbox-islombek";

    [HttpPost]
    public async Task Post(IFormFile formFile)
    {
        var client = new AmazonS3Client();
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(client, BucketName);
        if (!bucketExists)
        {
            var bucketRequest = new PutBucketRequest()
            {
                BucketName = BucketName,
                UseClientRegion = true
            };  

            await client.PutBucketAsync(bucketRequest);
        }
        var objectRequest = new PutObjectRequest()
        {
            BucketName = BucketName,
            Key = formFile.FileName,
            InputStream = formFile.OpenReadStream(),
        };
        var reponse = client.PutObjectAsync(objectRequest);

    }*/

    private readonly IStorageService _storageservice;
    private readonly IConfiguration _config;
    private readonly ILogger<AdminVideoController> _logger;
    public AdminVideoController(ILogger<AdminVideoController> logger,IConfiguration config, IStorageService storageservice)
    {
        _logger = logger;
        _storageservice = storageservice;
        _config = config;
    }

    [HttpPost(Name = "UploadFile")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // Process the file
        await using var memoryStr = new MemoryStream();
        await file.CopyToAsync(memoryStr);

        var fileExt = Path.GetExtension(file.FileName);
        var objName = $"{Guid.NewGuid()}.{fileExt}";

        var s3Obj = new S3Object()
        {
            BucketName = "abdisamadovich",
            InputStream = memoryStr,
            Name = objName
        };

        var cred = new AwsCredentials()
        {
            AwsKey = _config["AwsConfiguration:AWSAccessKey"],
            AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
        };

        var result = await _storageservice.UploadFileAsync(s3Obj, cred);

        return Ok(result);  
    }
}
