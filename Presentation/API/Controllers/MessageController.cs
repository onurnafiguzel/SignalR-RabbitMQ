using System.Text;
using System.Text.Json;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Post(User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://tzstczei:GFtaGndKJ2gCgdSyMbP01EozG2edaOOy@toad.rmq.cloudamqp.com/tzstczei");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messageQueue", false, false, false);

            string serializeData = JsonSerializer.Serialize(model);
            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messageQueue", body: data);

            return Ok();
        }
    }
}
