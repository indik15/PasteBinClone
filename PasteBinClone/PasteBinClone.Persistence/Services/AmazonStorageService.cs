using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using PasteBinClone.Application.Interfaces;

namespace PasteBinClone.Persistence.Services
{
    public class AmazonStorageService : IAmazonStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly AmazonS3Client _client;

        public AmazonStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _bucketName = _configuration["AWSBucket:BucketName"]!;
            _client = new AmazonS3Client(_configuration["AWSSecret:AccessKey"], _configuration["AWSSecret:SecretKey"], RegionEndpoint.EUNorth1);
        }
        public async Task<bool> DeleteFile(string id)
        {
            try
            {
                //Send request
                await _client.DeleteObjectAsync(_bucketName, id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRangeFiles(IEnumerable<string> keys)
        {
            try
            {
                var awsKeys = new List<KeyVersion>();

                foreach(var key in keys)
                {
                    awsKeys.Add(new KeyVersion { Key = key });
                }

                var deleteRequest = new DeleteObjectsRequest
                {
                    BucketName = _bucketName,
                    Objects = awsKeys
                };

                //Send request
                await _client.DeleteObjectsAsync(deleteRequest);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetFile(string id)
        {
            //Send request
            var result = await _client.GetObjectAsync(_bucketName, id);

            using var stream = new StreamReader(result.ResponseStream);

            //Read and return text from the Stream
            return await stream.ReadToEndAsync();
        }

        public async Task<bool> UpdateFile(string id, string body)
        {
            try
            {
                //Create a request
                var request = new PutObjectRequest
                {
                    Key = id,
                    ContentBody = body,
                    BucketName = _bucketName
                };

                //Send request
                await _client.PutObjectAsync(request);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(bool, string?)> UploadFile(string body)
        {
            string id = Guid.NewGuid().ToString();

            //Create a request
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = id,
                ContentBody = body,
            };

            //Send request
            await _client.PutObjectAsync(request);

            return (true, id);
        }
    }
}
