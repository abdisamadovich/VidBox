using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Mvc;

namespace VidBox.WebApi.Controllers.Adminstrator.Videos;

[Route("api/admin")]
[ApiController]
public class AdminVideoController : ControllerBase
{
    public string BucketName = "vidbox-islombek";

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

    }
}
