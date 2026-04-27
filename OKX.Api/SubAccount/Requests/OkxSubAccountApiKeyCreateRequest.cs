#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Create sub-account API key request
/// </summary>
public record OkxSubAccountApiKeyCreateRequest
{
    public string? SubAccountName { get; set; }
    public string? Label { get; set; }
    public string? Passphrase { get; set; }
    public bool? ReadPermission { get; set; }
    public bool? TradePermission { get; set; }
    public string? IpAddresses { get; set; }
}
#pragma warning restore CS1591
