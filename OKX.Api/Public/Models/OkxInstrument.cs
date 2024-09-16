using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Instrument
/// </summary>
public class OkxInstrument
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-SWAP
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Base currency
    /// </summary>
    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; }

    /// <summary>
    /// Quote currency
    /// </summary>
    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Settlement currency
    /// </summary>
    [JsonProperty("settleCcy")]
    public string SettlementCurrency { get; set; }

    /// <summary>
    /// Contract value
    /// </summary>
    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    /// <summary>
    /// Contract multiplier
    /// </summary>
    [JsonProperty("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    /// <summary>
    /// Contract value currency
    /// </summary>
    [JsonProperty("ctValCcy")]
    public string ContractValueCurrency { get; set; }

    /// <summary>
    /// Option type
    /// </summary>
    [JsonProperty("optType"), JsonConverter(typeof(OkxOptionTypeConverter))]
    public OkxOptionType? OptionType { get; set; }

    /// <summary>
    /// Strike price
    /// </summary>
    [JsonProperty("stk")]
    public decimal? StrikePrice { get; set; }


    /// <summary>
    /// Listing timestamp
    /// </summary>
    [JsonProperty("listTime")]
    public long? ListingTimestamp { get; set; }

    /// <summary>
    /// Listing time
    /// </summary>
    [JsonIgnore]
    public DateTime? ListingTime { get { return ListingTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Expiry timestamp
    /// </summary>
    [JsonProperty("expTime")]
    public long? ExpiryTimestamp { get; set; }

    /// <summary>
    /// Expiry time
    /// </summary>
    [JsonIgnore]
    public DateTime? ExpiryTime { get { return ExpiryTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Maximal leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Tick size
    /// </summary>
    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }

    /// <summary>
    /// Lot size
    /// </summary>
    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    /// <summary>
    /// Minimal order size
    /// </summary>
    [JsonProperty("minSz")]
    public decimal MinimumOrderSize { get; set; }

    /// <summary>
    /// Contract type
    /// </summary>
    [JsonProperty("ctType"), JsonConverter(typeof(OkxContractTypeConverter))]
    public OkxContractType? ContractType { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxInstrumentStateConverter))]
    public OkxInstrumentState State { get; set; }

    /// <summary>
    /// Trading rule types
    /// </summary>
    [JsonProperty("ruleType"), JsonConverter(typeof(OkxInstrumentRuleTypeConverter))]
    public OkxInstrumentRuleType? RuleType { get; set; }

    /// <summary>
    /// Maximal limit order size
    /// </summary>
    [JsonProperty("maxLmtSz")]
    public decimal? MaximumLimitOrderSize { get; set; }

    /// <summary>
    /// Maximum market order size
    /// </summary>
    [JsonProperty("maxMktSz")]
    public decimal? MaximumMarketOrderSize { get; set; }

    /// <summary>
    /// Maximal limit order size in USD
    /// </summary>
    [JsonProperty("maxLmtAmt")]
    public decimal? MaximumLimitOrderSizeInUsd { get; set; }

    /// <summary>
    /// Maximum market order size in USD
    /// </summary>
    [JsonProperty("maxMktAmt")]
    public decimal? MaximumMarketOrderSizeInUsd { get; set; }

    /// <summary>
    /// Maximum TWAP order size
    /// </summary>
    [JsonProperty("maxTwapSz")]
    public decimal? MaximumTwapOrderSize { get; set; }

    /// <summary>
    /// Maximum iceberg order size
    /// </summary>
    [JsonProperty("maxIcebergSz")]
    public decimal? MaximumIcebergOrderSize { get; set; }

    /// <summary>
    /// Maximum trigger order size
    /// </summary>
    [JsonProperty("maxTriggerSz")]
    public decimal? MaximumTriggerOrderSize { get; set; }

    /// <summary>
    /// Maximum stop order size
    /// </summary>
    [JsonProperty("maxStopSz")]
    public decimal? MaximumStopMarketSize { get; set; }

    /// <summary>
    /// Alias
    /// </summary>
    [Obsolete("Not recommended for use, users are encouraged to rely on the expTime field to determine the delivery time of the contract")]
    [JsonProperty("alias"), JsonConverter(typeof(OkxInstrumentAliasConverter))]
    public OkxInstrumentAlias? Alias { get; set; }
}
