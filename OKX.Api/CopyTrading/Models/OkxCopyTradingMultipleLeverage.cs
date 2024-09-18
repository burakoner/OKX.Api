using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxMultipleLeverage
/// </summary>
public class OkxCopyTradingMultipleLeverage
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Lead trader leverages
    /// </summary>
    [JsonProperty("leadTraderLevers")]
    public List<LeverageData> LeadTraderLeverages { get; set; }

    /// <summary>
    /// My leverages
    /// </summary>
    [JsonProperty("myLevers")]
    public List<LeverageData> MyLeverages { get; set; }
}

/// <summary>
/// LeverageData
/// </summary>
public class LeverageData()
{
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }
}
