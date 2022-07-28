using System.Text;
using System.Text.Json;
using EMailSender;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

HubConnection connectionSignalR = new HubConnectionBuilder().WithUrl("https://localhost:7232/messagehub").Build();
await connectionSignalR.StartAsync();

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://tzstczei:GFtaGndKJ2gCgdSyMbP01EozG2edaOOy@toad.rmq.cloudamqp.com/tzstczei");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare("messageQueue", false, false, false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("messageQueue", true, consumer);

consumer.Received += async (s, e) =>
{
    //Email operasyonu burada gerçekleştirilecektir. ->

    string serializeData = Encoding.UTF8.GetString(e.Body.Span);
    User user = JsonSerializer.Deserialize<User>(serializeData);

    EMailSend.Send(user.Email, user.Message);
    Console.WriteLine($"{user.Email} EMailSend gönderilmiştir.");

    await connectionSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} EMailSend gönderilmiştir.");
};

Console.Read();
