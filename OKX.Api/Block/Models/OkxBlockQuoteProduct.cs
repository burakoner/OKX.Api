namespace OKX.Api.Block;

/// <summary>
/// OKX Block Quote Product
/// </summary>
public class OkxBlockQuoteProduct
{
    /// <summary>
    /// Type of instrument. Valid value can be FUTURES, OPTION, SWAP or SPOT.
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Receive all instruments or not under specific instType setting.
    /// Valid value can be boolean (True/False). By default, the value will be false.
    /// </summary>
    [JsonProperty("includeAll")]
    public bool IncludeAll { get; set; }

    /// <summary>
    /// Elements of the instType.
    /// </summary>
    [JsonProperty("data")]
    public List<OkxBlockQuoteProductData> Data { get; set; } = [];
}

/// <summary>
/// OKX Block Quote Product Data
/// </summary>
public class OkxBlockQuoteProductData
{
    /// <summary>
    /// Instrument family. Required for FUTURES, OPTION and SWAP only.
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Instrument ID. Required for SPOT only.
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Max trade quantity for the product(s).
    /// For FUTURES, OPTION and SWAP, the max quantity of the RFQ/Quote is in unit of contracts. For SPOT, this parameter is in base currency.
    /// </summary>
    [JsonProperty("maxBlockSz")]
    public decimal? MaximumBlockSize { get; set; }

    /// <summary>
    /// Price bands in unit of ticks, measured against mark price.
    /// Setting makerPxBand to 1 tick means:
    /// If Bid price &gt; Mark + 1 tick, it will be stopped
    /// If Ask price &lt; Mark - 1 tick, It will be stopped
    /// </summary>
    [JsonProperty("makerPxBand")]
    public decimal? MakerPriceBand { get; set; }
}
