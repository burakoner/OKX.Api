#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Reset sub-account API key request
/// </summary>
public record OkxSubAccountApiKeyResetRequest
{
    public string? SubAccountName { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiLabel { get; set; }
    public bool? ReadPermission { get; set; }
    public bool? TradePermission { get; set; }
    public string? IpAddresses { get; set; }
}
#pragma warning restore CS1591
