using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.DTO;
using Domain.Interfaces;
using Infrastructure.Redis;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Redis
{
    public class UpdateCacheBackgroundService : BackgroundService
    {
        private readonly ILogger<UpdateCacheBackgroundService> _logger;
        private readonly IServiceProvider _service;
        private IArticleRepository _repository;
        private RedisService _redisService;

        public UpdateCacheBackgroundService(ILogger<UpdateCacheBackgroundService> logger,
            IServiceProvider service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           while (!stoppingToken.IsCancellationRequested)
           {
               _logger.LogError("Updating cache in background");
               await UpdateCache();
               _logger.LogError("Cache Updated");
           }
        }
        
        private async Task UpdateCache()
        {
            using (var scope = _service.CreateScope())
            {
                _redisService = scope.ServiceProvider.GetRequiredService<RedisService>();
                _repository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();
                
                var articles = _repository.List()
                    .ToList();
                
                var redisObjectList = articles
                    .Select(art => new ArticleCachedDTO(art.Id, art.Content, art.Description, art.Likes.Count))
                    .ToList();
                
                await _redisService.Update(redisObjectList);
            
                await Task.Delay(TimeSpan.FromSeconds(20));
            }
        }
    }
}