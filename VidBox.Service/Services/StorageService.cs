using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using VidBox.Service.Services.Models;

namespace VidBox.Service.Services;

public class StorageService : IStorageService
{
    public async Task<S3ResponseDto> UploadFileAsync(S3Object s3Obj, AwsCredentials awsCredentials)
    {
        // Adding AWS credentials
        var credentials = new BasicAWSCredentials(awsCredentials.AwsKey,awsCredentials.AwsSecretKey);

        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.APSouth1
        };

        var response = new S3ResponseDto();

        try
        {
            // Create the upload request
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3Obj.InputStream,
                Key = s3Obj.Name,
                BucketName = s3Obj.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            // Created an S3 client 
            using var client = new AmazonS3Client(credentials,config);
            
            // Upload utility to S3 
            var transferUtility = new TransferUtility(client);

            // We are actually uploading the file to S3
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Message = $"{s3Obj.Name} has been uploaded successfully";
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
}
