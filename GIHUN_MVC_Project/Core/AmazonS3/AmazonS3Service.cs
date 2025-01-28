using Amazon.S3;
using Amazon.S3.Model;
using GIHUN_MVC_Project.Models.AmazonS3;

namespace GIHUN_MVC_Project.Core.AmazonS3
{
    public class AmazonS3Service : IAmazonS3Repository
    {
        private readonly IAmazonS3 _s3Client;

        public AmazonS3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        /// <summary>
        /// 이미지 파일 업로드
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            string bucketName = "gihun-mvc-project-profile-image";
            string fileKey = file.FileName;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileKey,
                    InputStream = memoryStream
                };
                putRequest.Metadata.Add("Content-Type", file.ContentType);

                var response = await _s3Client.PutObjectAsync(putRequest);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 버킷 생성
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> CreateBucket(string bucketName)
        {
            var isExists = isExistsBucket(bucketName);

            if (isExists == true)
            {
                return false;
            }

            await _s3Client.PutBucketAsync(bucketName);
            return true;
        }


        /// <summary>
        /// 버킷 삭제
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteBucket(string bucketName)
        {
            var isExists = isExistsBucket(bucketName);

            if (isExists == true)
            {
                await _s3Client.DeleteBucketAsync(bucketName);

                return true;
            }
            return false;

        }


        /// <summary>
        /// 파일 삭제
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFile(string bucketName, string key)
        {
            var isExists = isExistsBucket(bucketName);

            if (isExists == true)
            {
                await _s3Client.DeleteObjectAsync(bucketName, key);

                return true;
            }

            return false;
        }


        /// <summary>
        /// 버킷에 들어 있는 모든 파일들을 가져옵니다.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task<List<AmazonS3Object>> GetAllFile(string bucketName, string? prefix)
        {
            var isExists = isExistsBucket(bucketName);

            if (isExists == false)
                return new();

            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };

            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Object = result.S3Objects.Select(x =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = x.Key,
                    Expires = DateTime.UtcNow.AddYears(1)
                };

                return new AmazonS3Object()
                {
                    Name = x.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest)
                };
            }).ToList();

            return s3Object;
        }


        /// <summary>
        /// 아마존 S3에 있는 버킷들의 목록들을 다 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public async Task<List<S3Bucket>> GetBucketList()
        {
            var bucketList = await _s3Client.ListBucketsAsync();
            var buckets = bucketList.Buckets;

            return buckets;
        }


        /// <summary>
        /// 버킷과 키를 사용하여 이미지파일을 가져옵니다.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<GetObjectResponse> GetFileByKey(string key, string? bucketName = "gihun-mvc-project-profile-image")
        {
            var isExists = isExistsBucket(bucketName);

            if (isExists == false)
                return new();

            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);

            return s3Object;
        }

        /// <summary>
        /// 버킷이 존재하는지 알려주는 함수
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        private bool isExistsBucket(string bucketName)
        {
            var isExists = Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName).Result;

            if (isExists == true)
            {
                return true;
            }

            return false;
        }



    }
}
