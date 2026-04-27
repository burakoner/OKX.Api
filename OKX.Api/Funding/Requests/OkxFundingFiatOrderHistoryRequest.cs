#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Fiat order history query request
/// </summary>
public record OkxFundingFiatOrderHistoryRequest
{
    public string? Currency { get; set; }
    public string? PaymentMethod { get; set; }
    public string? State { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
