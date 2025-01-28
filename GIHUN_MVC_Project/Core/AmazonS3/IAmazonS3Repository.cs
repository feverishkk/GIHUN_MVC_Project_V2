using Amazon.S3.Model;
using GIHUN_MVC_Project.Models.AmazonS3;

namespace GIHUN_MVC_Project.Core.AmazonS3
{
    public interface IAmazonS3Repository
    {
        Task<bool> UploadFile(IFormFile file);
        Task<List<S3Bucket>> GetBucketList();
        Task<bool> CreateBucket(string bucketName);
        Task<bool> DeleteBucket(string bucketName);
        Task<List<AmazonS3Object>> GetAllFile(string bucketName, string? prefix);
        Task<GetObjectResponse> GetFileByKey(string key, string? bucketName = "gihun-mvc-project-profile-image");
        Task<bool> DeleteFile(string bucketName, string key);
    }
}
