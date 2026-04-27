#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Sub-account bill query request
/// </summary>
public record OkxSubAccountBillQueryRequest
{
    public string? SubAccountName { get; set; }
    public string? Currency { get; set; }
    public OkxSubAccountTransferType? Type { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
