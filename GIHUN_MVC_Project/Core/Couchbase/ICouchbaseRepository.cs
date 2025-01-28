using Couchbase.Management.Buckets;
using GIHUN_MVC_Project.Models.Hotels;

namespace GIHUN_MVC_Project.Core.Couchbase
{
    public interface ICouchbaseRepository
    {
        Task<Dictionary<string, BucketSettings>> GetAllBucketList();
        Task<SaveHotelSummary> GetById(string id);
        Task Upsert(SaveHotelSummary model);
        Task Delete(string id);
        Task SaveUserSearchedFromAPI();
        Task<List<SaveHotelSummary>> GetAllBucketData();
        Task<List<SaveHotelSummary>> GetAllCacheData();
        Task SetProfileImageCache(string userEmail, string profileImageName);
        Task<string> GetProfileImageCache(string userEmail);
        Task InsertLog(string bucketName, string userEmail, string log, dynamic extra, string suffix);
    }
}
