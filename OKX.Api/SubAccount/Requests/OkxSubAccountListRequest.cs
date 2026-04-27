#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Sub-account list query request
/// </summary>
public record OkxSubAccountListRequest
{
    public bool? Enable { get; set; }
    public string? SubAccountName { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
