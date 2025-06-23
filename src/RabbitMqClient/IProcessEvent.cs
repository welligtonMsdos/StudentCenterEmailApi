namespace StudentCenterEmailApi.src.RabbitMqClient;

public interface IProcessEvent
{
    void Process(string message);
}
