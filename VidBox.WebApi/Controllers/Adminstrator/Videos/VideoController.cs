using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VidBox.WebApi.Controllers.Adminstrator.Videos
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
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
        }
    }
}
    