using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Models.Public;

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
    public string Instrument { get; set; }

    /// <summary>
    /// Underlying, e.g. BTC-USD. Only applicable to FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    [JsonProperty("baseCcy")]
    public string BaseCurrency { get; set; }

    [JsonProperty("quoteCcy")]
    public string QuoteCurrency { get; set; }

    [JsonProperty("settleCcy")]
    public string SettlementCurrency { get; set; }

    [JsonProperty("ctVal")]
    public decimal? ContractValue { get; set; }

    [JsonProperty("ctMult")]
    public decimal? ContractMultiplier { get; set; }

    [JsonProperty("ctValCcy")]
    public string ContractValueCurrency { get; set; }

    [JsonProperty("optType"), JsonConverter(typeof(OptionTypeConverter))]
    public OkxOptionType? OptionType { get; set; }

    [JsonProperty("stk")]
    public decimal? StrikePrice { get; set; }

    [JsonProperty("listTime")]
    public long? ListingTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? ListingTime { get { return ListingTimestamp?.ConvertFromMilliseconds(); } }

    [JsonProperty("expTime")]
    public long? ExpiryTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? ExpiryTime { get { return ExpiryTimestamp?.ConvertFromMilliseconds(); } }

    [JsonProperty("lever")]
    public decimal? MaximumLeverage { get; set; }

    [JsonProperty("tickSz")]
    public decimal TickSize { get; set; }

    [JsonProperty("lotSz")]
    public decimal LotSize { get; set; }

    [JsonProperty("minSz")]
    public decimal MinimumOrderSize { get; set; }

    [JsonProperty("ctType"), JsonConverter(typeof(OkxContractTypeConverter))]
    public OkxContractType? ContractType { get; set; }

    [JsonProperty("alias"), JsonConverter(typeof(InstrumentAliasConverter))]
    public OkxInstrumentAlias? Alias { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(InstrumentStateConverter))]
    public OkxInstrumentState State { get; set; }

    [JsonProperty("maxLmtSz")]
    public decimal? MaximumLimitOrderSize { get; set; }

    [JsonProperty("maxMktSz")]
    public decimal? MaximumMarketOrderSize { get; set; }

    [JsonProperty("maxTwapSz")]
    public decimal? MaximumTwapOrderSize { get; set; }

    [JsonProperty("maxIcebergSz")]
    public decimal? MaximumIcebergOrderSize { get; set; }

    [JsonProperty("maxTriggerSz")]
    public decimal? MaximumTriggerOrderSize { get; set; }

    [JsonProperty("maxStopSz")]
    public decimal? MaximumStopMarketSize { get; set; }
}
