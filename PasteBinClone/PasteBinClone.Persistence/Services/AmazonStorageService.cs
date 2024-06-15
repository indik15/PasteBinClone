﻿using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using PasteBinClone.Application.Interfaces;

namespace PasteBinClone.Persistence.Services
{
    public class AmazonStorageService : IAmazonStorageService
    {
        private readonly IAmazonS3 _awsClient;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;

        public AmazonStorageService(IAmazonS3 awsClient,
        IConfiguration configuration)
        {
            _awsClient = awsClient;
            _configuration = configuration;
            _bucketName = _configuration["AWS:BucketName"]!;
        }
        public async Task<bool> DeleteFile(string id)
        {
            try
            {
                await _awsClient.DeleteObjectAsync(_bucketName, id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetFile(string id)
        {
            try
            {
                var result = await _awsClient.GetObjectAsync(_bucketName, id);

                using var stream = new StreamReader(result.ResponseStream);

                return await stream.ReadToEndAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateFile(string id, string body)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    Key = id,
                    ContentBody = body,
                    BucketName = _bucketName
                };

                await _awsClient.PutObjectAsync(request);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<(bool, string?)> UploadFile(string body)
        {          
            try
            {
                string id = Guid.NewGuid().ToString();

                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
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
