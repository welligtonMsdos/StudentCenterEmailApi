using Microsoft.AspNetCore.Mvc;
using StudentCenterEmailApi.src.Application.DTOs;
using StudentCenterEmailApi.src.Application.Interfaces;

namespace StudentCenterEmailApi.src.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmailController : BaseController
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("[Action]")]
    public async Task<IActionResult> SendEmail()
    {
        try
        {
            var newUser = new UserDto("67f42b4a406f3f471ac3ebcf", 
                                      "Welligton Silva", 
                                      "WelligtonAndreSilva@gmail.com", 
                                      "$2a$11$XQV9Rfdhp5ywBRbPBOp16.Cv8KYusB7VZI1CSWhpJ3egRxqioupSm");

            return Ok(await _emailService.SendEmail(newUser));
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }
}
