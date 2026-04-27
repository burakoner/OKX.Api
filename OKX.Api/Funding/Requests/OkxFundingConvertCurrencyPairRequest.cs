#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Convert currency pair query request
/// </summary>
public record OkxFundingConvertCurrencyPairRequest
{
    public string? FromCurrency { get; set; }
    public string? ToCurrency { get; set; }
    public bool? UseLargeOrderConvert { get; set; }
}
#pragma warning restore CS1591
