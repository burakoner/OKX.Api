namespace OKX.Api.Account;

/// <summary>
/// Demo account balance adjustment request.
/// </summary>
public record OkxAccountDemoBalanceAdjustmentRequest
{
    /// <summary>
    /// Direction of the balance adjustment.
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAccountDemoBalanceAdjustmentType Type { get; set; }

    /// <summary>
    /// Currency adjustments. At least one item is required and duplicate currencies are not allowed.
    /// </summary>
    [JsonProperty("adjustments")]
    public IEnumerable<OkxAccountDemoBalanceAdjustmentItemRequest> Adjustments { get; set; } = [];
}

/// <summary>
/// Currency amount in a demo account balance adjustment request.
/// </summary>
public record OkxAccountDemoBalanceAdjustmentItemRequest
{
    /// <summary>
    /// Currency. Supported values are BTC, ETH, USDT, and OKB.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Non-negative adjustment amount.
    /// </summary>
    [JsonProperty("amt")]
    [JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal Amount { get; set; }
}
