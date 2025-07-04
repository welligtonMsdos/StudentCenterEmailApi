using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace StudentCenterEmailApi.src.RabbitMqClient;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IProcessEvent _processEvent;

    public RabbitMqSubscriber(IConfiguration configuration, IProcessEvent processEvent)
    {
        _configuration = configuration;

        _connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
            UserName = _configuration["RabbitMQUser"],
            Password = _configuration["RabbitMQPassword"]
        }.CreateConnection();

        _channel = _connection.CreateModel();

        _queueName = "trigger";

        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
         );

        _channel.ExchangeDeclare(
            exchange: "trigger",
            type: ExchangeType.Fanout,
            durable: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueBind(
            queue: _queueName,
            exchange: "trigger",
            routingKey: ""
        );

        _processEvent = processEvent;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        EventingBasicConsumer? consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            ReadOnlyMemory<byte> body = ea.Body;

            string? message = Encoding.UTF8.GetString(body.ToArray());

            _processEvent.Process(message);

        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
