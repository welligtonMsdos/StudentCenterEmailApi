using AutoMapper;
using StudentCenterEmailApi.src.Application.DTOs;
using StudentCenterEmailApi.src.Application.Interfaces;
using System.Text.Json;

namespace StudentCenterEmailApi.src.RabbitMqClient;

public class ProcessEvent : IProcessEvent
{   
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessEvent(IServiceScopeFactory serviceScopeFactory)
    {       
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Process(string message)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var emailRepository = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var userDto = JsonSerializer.Deserialize<UserDto>(message);

        emailRepository.SendEmail(userDto);
    }
}
