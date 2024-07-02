namespace OKX.Api.Common.Models;

/// <summary>
/// OKX Rest API Error
/// </summary>
/// <remarks>
/// OKX Rest API Error
/// </remarks>
/// <param name="code">Error Code</param>
/// <param name="message">Error Message</param>
/// <param name="data">Error Data</param>
public class OkxRestApiError(int? code, string message, object data) : Error(code, message, data)
{
}