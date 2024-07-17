using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trading.Converters;
using OKX.Api.Trading.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxMaximumLoanAmount
/// </summary>
public class OkxMaximumLoanAmount
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxMarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    /// <summary>
    /// Margin currency
    /// </summary>
    [JsonProperty("mgnCcy")]
    public string MarginCurrency { get; set; }

    /// <summary>
    /// Max loan
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal MaximumLoan { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }
}
