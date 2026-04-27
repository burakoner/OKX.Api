#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Create sub-account request
/// </summary>
public record OkxSubAccountCreateRequest
{
    public string? SubAccountName { get; set; }
    public OkxSubAccountType Type { get; set; }
    public string? Label { get; set; }
    public string? Password { get; set; }
}
#pragma warning restore CS1591
