using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using PasteBinClone.Application.Interfaces;

namespace PasteBinClone.Persistence.Services
{
    public class AmazonStorageService(IAmazonS3 awsClient,
        IConfiguration configuration) : IAmazonStorageService
    {
        private readonly IAmazonS3 _awsClient = awsClient;
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> GetFile(string id)
        {
            string bucketName = _configuration["AWS:BucketName"];

            var result = await _awsClient.GetObjectAsync(bucketName, id);

            using var stream = new StreamReader(result.ResponseStream);

            return await stream.ReadToEndAsync();
        }

        public async Task<(bool, string?)> UploadFile(string body)
        {          
            try
            {
                string id = Guid.NewGuid().ToString();
                string bucketName = _configuration["AWS:BucketName"];

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = id,
                    ContentBody = body,
                };

                await _awsClient.PutObjectAsync(request);

                return (true, id);
            }
            catch (Exception)
            {
                return (false, null);
            }
        }
    }
}
