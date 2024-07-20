using OKX.Api.SignalBotTrading.Converters;
using OKX.Api.SignalBotTrading.Enums;

namespace OKX.Api.SignalBotTrading.Models;

/// <summary>
/// OKX Signal Exit Paramaters
/// </summary>
public class OkxSignalExitParamaters
{
    /// <summary>
    /// Type of set the take-profit and stop-loss trigger price
    /// </summary>
    [JsonProperty("tpSlType"), JsonConverter(typeof(OkxSignalTpslTriggerPriceConverter))]
    public OkxSignalTpslTriggerPrice TakeProfitStopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit percentage
    /// </summary>
    [JsonProperty("tpPct")]
    public string TakeProfitPercentage { get; set; }

    /// <summary>
    /// Stop-loss percentage
    /// </summary>
    [JsonProperty("slPct")]
    public string StopLossPercentage { get; set; }
}