using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Application.Interfaces;
using Application.Interfaces.DTO;

namespace Infrastructure.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string _key;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _key = "Articles";
        }

        public async Task Update(IList<ArticleCachedDTO> articles)
        {
            await _distributedCache.RemoveAsync(_key);

            var jsonString = JsonSerializer.Serialize(articles.ToList());

            await _distributedCache.SetStringAsync(_key, jsonString);
        }

        public async Task<List<ArticleCachedDTO>> Read()
        {
            var dataCached = await _distributedCache.GetStringAsync(_key);
            var list = new List<ArticleCachedDTO>();
            
            if (dataCached != null)
            {
                list = JsonSerializer.Deserialize<List<ArticleCachedDTO>>(dataCached);
            }

            return list;
        }
        
        public async Task<ArticleCachedDTO> ReadOne(long articleId)
        {
            var list = await Read();

            return list?.FirstOrDefault(x => x.Id == articleId);
        }
    }
}