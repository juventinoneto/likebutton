using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.DTO;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;


namespace Infrastructure.Queu
{
    public class SubscriberService : BackgroundService
    {
        private readonly IServiceProvider _service;
        private ArticleService _articleService;
        private readonly IModel _channel;

        public SubscriberService(IServiceProvider service)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            
            _channel = connection.CreateModel();
            _channel.QueueDeclare(
                queue: "like_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _service = service;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<LikeDTO>(contentString);
                RegisterLike(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            
            _channel.BasicConsume(queue:"like_queue", autoAck: false, consumer: consumer);
            await Task.Delay(TimeSpan.FromSeconds(30));
            //return Task.CompletedTask;
        }

        private void RegisterLike(LikeDTO like)
        {
            using (var scope = _service.CreateScope())
            {
                _articleService = scope.ServiceProvider.GetRequiredService<ArticleService>();
                _articleService.LikeArticle(like.ArticleId);
            }
        }
    }
}