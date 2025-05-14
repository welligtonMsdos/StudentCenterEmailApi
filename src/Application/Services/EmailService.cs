using StudentCenterEmailApi.src.Application.DTOs;
using StudentCenterEmailApi.src.Application.Interfaces;
using StudentCenterEmailApi.src.Infrastructure.Utils;
using System.Net;
using System.Net.Mail;


namespace StudentCenterEmailApi.src.Application.Services;

public class EmailService : IEmailService
{
    public EmailService()
    {
    }

    public Task<bool> SendEmail(UserDto userDto)
    {
        try
        {
            var email = Util.GetEmail().Result;

            var password = Util.GetPassword().Result;

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            var body = "Este é um e-mail de teste enviado via Outlook SMTP. \n\n" +
                       "Nome: " + userDto.Name + "\n" +
                       "E-mail: " + userDto.Email + "\n" +
                       "Senha: " + userDto.PassWord + "\n\n" +
                       "Atenciosamente,\n" +
                       "Equipe Student Center Email API";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = "Student Center",
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(userDto.Email);

            smtpClient.Send(mailMessage);

            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }
    }
}
