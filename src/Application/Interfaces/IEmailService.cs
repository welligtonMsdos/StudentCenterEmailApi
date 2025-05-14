using StudentCenterEmailApi.src.Application.DTOs;

namespace StudentCenterEmailApi.src.Application.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmail(UserDto userDto);
}
