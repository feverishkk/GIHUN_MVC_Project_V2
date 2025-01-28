using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Management.Buckets;
using GIHUN_MVC_Project.Models.Hotels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GIHUN_MVC_Project.Core.Couchbase
{
    public class CouchbaseService : ICouchbaseRepository
    {
        private readonly IBucketProvider _bucketProvider;
        private readonly ICouchbaseCache _couchbaseCache;

        public CouchbaseService(IBucketProvider bucketProvider, ICouchbaseCache couchbaseCache)
        {
            _bucketProvider = bucketProvider;
            _couchbaseCache = couchbaseCache;
        }

        public async Task<Dictionary<string, BucketSettings>> GetAllBucketList()
        {
            var bucket = await _bucketProvider.GetBucketAsync("");
            var cluster = bucket.Cluster;
            var allBuckets = await cluster.Buckets.GetAllBucketsAsync();

            return allBuckets;
        }

        public async Task<SaveHotelSummary> GetById(string id)
        {
            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var cluster = bucket.Cluster;
            var collection = await bucket.CollectionAsync("_default");

            var item = await collection.GetAsync(id);
            var result = item.ContentAs<SaveHotelSummary>();

            return result;
        }

        public async Task Upsert(SaveHotelSummary model)
        {
            if (model.Id == null || string.IsNullOrEmpty(model.Id))
            {
                return;
            }

            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var cluster = bucket.Cluster;
            var collection = await bucket.CollectionAsync("_default");

            await collection.UpsertAsync(model.Id, model);

        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || id == null)
            {
                return;
            }

            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var collection = bucket.Collection("_default");

            await collection.RemoveAsync(id);
        }

        public async Task SaveUserSearchedFromAPI()
        {
            Root result = new();
            string responseStr = "";
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://hotels-com6.p.rapidapi.com/hotels/details-summary?propertyId=536_3856166"),
                Headers =
                {
                    { "x-rapidapi-key", "" },
                    { "x-rapidapi-host", "" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                responseStr = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<Root>(responseStr);
            }

            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var collection = bucket.Collection("_default");

            var mainStream = result.data;
            var eachData = JsonSerializer.Serialize(mainStream);

            SaveHotelSummary save = new()
            {
                Id = result.data.summary.id.ToString(),
                Name = result.data.summary.name.ToString(),
                Latitude = result.data.summary.location.coordinates.latitude.ToString(),
                Longitude = result.data.summary.location.coordinates.longitude.ToString(),
                HotelAddress = result.data.summary.location.address.addressLine.ToString(),
                Value = result.data.reviewInfo.summary.overallScoreWithDescriptionA11y.value.ToString(),
                Json_OG = result.data.summary
            };

            await collection.UpsertAsync(save.Id, save);

            return;
        }

        public async Task<List<SaveHotelSummary>> GetAllBucketData()
        {
            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var cluster = bucket.Cluster;

            var result = await cluster.QueryAsync<SaveHotelSummary>("SELECT h.id, h.name, h.`value`, h.hotelAddress, h.latitude, h.longitude " +
                                                                    "FROM `HotelSummary`.`_default`.`_default` h " +
                                                                    "LIMIT 10").Result.ToListAsync();

            return result;
        }

        /// <summary>
        /// 캐시처럼 사용한다.
        /// </summary>
        /// <returns></returns>
        public async Task<List<SaveHotelSummary>> GetAllCacheData()
        {
            var bucket = await _bucketProvider.GetBucketAsync("HotelSummary");
            var cluster = bucket.Cluster;

            string query = "SELECT h.name, h.id, h.photo1 " +
                           "FROM `HotelSummary`.`_default`.`_default` h " +
                           "ORDER BY h.id DESC " +
                           "LIMIT 2";

            var result = await cluster.QueryAsync<SaveHotelSummary>(query).Result.ToListAsync();

            return result;
        }

        public async Task SetProfileImageCache(string userEmail, string profileImageName)
        {
            await _couchbaseCache.SetAsync(userEmail, profileImageName, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(999)));
        }

        public async Task<string> GetProfileImageCache(string userEmail)
        {
            var result = await _couchbaseCache.GetAsync<string>(userEmail);


            return result;
        }

        public async Task InsertLog(string bucketName, string userEmail, string log, dynamic extra, string suffix)
        {
            var bucket = await _bucketProvider.GetBucketAsync(bucketName);
            var cluster = bucket.Cluster;
            var collection = await bucket.CollectionAsync("_default");

            var randomNum = new Random();
            string id = userEmail + " - " + suffix + " - " + randomNum.Next().ToString();
            string content = bucketName + " - " + userEmail + " - " + log + " - " + extra + " - " + DateTime.Now.ToString("yyyy-MM-dd/hh:ss") + " - " + suffix;

            await collection.InsertAsync(id, content);
        }






    }
}