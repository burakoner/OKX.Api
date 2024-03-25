using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Option Market Data
/// </summary>
public class OkxOptionSummary
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-200103-5500-C
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    /// <summary>
    /// Sensitivity of option price to uly price
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// The delta is sensitivity to uly price
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Sensitivity of option price to implied volatility
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Sensitivity of option price to remaining maturity
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Sensitivity of option price to uly price in BS mode
    /// </summary>
    [JsonProperty("deltaBS")]
    public decimal DeltaBS { get; set; }

    /// <summary>
    /// The delta is sensitivity to uly price in BS mode
    /// </summary>
    [JsonProperty("gammaBS")]
    public decimal GammaBS { get; set; }

    /// <summary>
    /// Sensitivity of option price to implied volatility in BS mode
    /// </summary>
    [JsonProperty("vegaBS")]
    public decimal VegaBS { get; set; }

    /// <summary>
    /// Sensitivity of option price to remaining maturity in BS mode
    /// </summary>
    [JsonProperty("thetaBS")]
    public decimal ThetaBS { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Mark volatility
    /// </summary>
    [JsonProperty("markVol")]
    public decimal? MarkVolatility { get; set; }

    /// <summary>
    /// Bid volatility
    /// </summary>
    [JsonProperty("bidVol")]
    public decimal? BidVolatility { get; set; }

    /// <summary>
    /// Ask volatility
    /// </summary>
    [JsonProperty("askVol")]
    public decimal? AskVolatility { get; set; }

    /// <summary>
    /// Realized volatility (not currently used)
    /// </summary>
    [JsonProperty("realVol")]
    public decimal? RealVolatility { get; set; }

    /// <summary>
    /// Implied volatility of at-the-money options
    /// </summary>
    [JsonProperty("volLv")]
    public decimal? ImpliedVolatility { get; set; }

    /// <summary>
    /// Forward price
    /// </summary>
    [JsonProperty("fwdPx")]
    public decimal? ForwardPrice { get; set; }

    /// <summary>
    /// Data update time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data update time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
