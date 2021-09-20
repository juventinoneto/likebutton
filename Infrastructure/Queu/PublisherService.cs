using System.Text;
using Application.Interfaces;
using Application.Interfaces.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Queu
{
    public class PublisherService : IPublisherService
    {
        private readonly ConnectionFactory _factory;

        public PublisherService()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }
        
        public void Publish(long articleId)
        {
            using (var connection = _factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "like_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var message = new LikeDTO() { ArticleId = articleId};
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(exchange: "", routingKey: "like_queue", basicProperties: null, body: body);
                }
            }
        }
    }
}