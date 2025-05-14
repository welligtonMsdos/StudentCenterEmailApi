namespace StudentCenterEmailApi.src.Infrastructure.Utils;

public static class Util
{
    public static Task<string> GetEmail()
    {
        try
        {
            var email = "";

            return Task.FromResult(email);
        }
        catch (Exception)
        {
            return Task.FromResult("email not found");
        }
    }

    public static Task<string> GetPassword() 
    {
        try
        {
            var password = "";

            return Task.FromResult(password);
        }
        catch (Exception)
        {
            return Task.FromResult("email not found");
        }
    }
}
