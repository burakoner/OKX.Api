using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxMarginBalance
/// </summary>
public class OkxMarginBalance
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Position side, long short
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Amount to be increase or decrease
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// add: add margin, or transfer collaterals in (Quick Margin Mode)
    /// reduce: reduce margin, transfer collaterals out (Quick Margin Mode)
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxMarginAddReduceConverter))]
    public OkxMarginAddReduce MarginAddReduce { get; set; }

    /// <summary>
    /// Real leverage after the margin adjustment
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Currency, only applicable to MARGIN（Manual transfers and Quick Margin Mode）
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
}
