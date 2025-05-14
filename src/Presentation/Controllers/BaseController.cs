using Microsoft.AspNetCore.Mvc;

namespace StudentCenterEmailApi.src.Presentation.Controllers;

public class BaseController : Controller
{
    protected BaseController() { }

    protected ActionResult Sucess(object result, bool isUpdate = false)
    {
        return Ok(new
        {
            success = true,
            message = isUpdate ? "Atualizado com sucesso" : "Salvo com sucesso",
            data = result
        });
    }

    protected ActionResult Sucess(string msg)
    {
        return Ok(new
        {
            success = true,
            message = msg
        });
    }

    protected ActionResult Error(Exception ex)
    {
        if (ex.InnerException == null)
        {
            return Ok(new
            {
                success = false,
                message = ex.Message
            });
        }

        return Ok(new
        {
            success = false,
            message = ex.InnerException.Message
        });
    }

    protected ActionResult Error(string erro)
    {
        return Ok(new
        {
            success = false,
            message = erro
        });
    }
}
