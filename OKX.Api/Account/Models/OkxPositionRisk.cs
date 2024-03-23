using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxPositionRisk
/// </summary>
public class OkxPositionRisk
{
    /// <summary>
    /// Update time of account information, millisecond format of Unix timestamp, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time of account information
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Adjusted / Effective equity in USD
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Detailed asset information in all currencies
    /// </summary>
    [JsonProperty("balData")]
    public List<OkxAccountPositionRiskBalanceData> BalanceData { get; set; }

    /// <summary>
    /// Detailed position information in all currencies
    /// </summary>
    [JsonProperty("posData")]
    public List<OkxAccountPositionRiskPositionData> PositionData { get; set; }
}

/// <summary>
/// OkxAccountPositionRiskBalanceData
/// </summary>
public class OkxAccountPositionRiskBalanceData
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Equity of the currency
    /// </summary>
    [JsonProperty("eq")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// Discount equity of the currency in USD.
    /// </summary>
    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }
}

/// <summary>
/// OkxAccountPositionRiskPositionData
/// </summary>
public class OkxAccountPositionRiskPositionData
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxMarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-180216
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Quantity of positions contract. In the mode of autonomous transfer from position to position, after the deposit is transferred, a position with pos of 0 will be generated
    /// </summary>
    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Base currency balance, only applicable to MARGIN（Manual transfers and Quick Margin Mode）
    /// </summary>
    [JsonProperty("baseBal")]
    public decimal? BaseBalance { get; set; }

    /// <summary>
    /// Quote currency balance, only applicable to MARGIN（Manual transfers and Quick Margin Mode）
    /// </summary>
    [JsonProperty("quoteBal")]
    public decimal? QuoteBalance { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    /// <summary>
    /// Position currency, only applicable to MARGIN positions.
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; }

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Notional value of positions in coin
    /// </summary>
    [JsonProperty("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    /// <summary>
    /// Notional value of positions in USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }
}
