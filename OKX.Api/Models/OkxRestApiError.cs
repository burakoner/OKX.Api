namespace OKX.Api.Models;

public class OkxRestApiError : Error
{
    public OkxRestApiError(int? code, string message, object data) : base(code, message, data)
    {
    }
}