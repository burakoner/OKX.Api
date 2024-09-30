namespace OKX.Api.Funding;

/// <summary>
/// OKX Non Tradable Asset Balance
/// </summary>
public record OkxFundingNonTradableAssetBalance
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Logo Link
    /// </summary>
    [JsonProperty("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// Available
    /// </summary>
    [JsonProperty("bal")]
    public decimal Available { get; set; }

    /// <summary>
    /// Withdrawable
    /// </summary>
    [JsonProperty("canWd")]
    public bool Withdrawable  { get; set; }

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; } = string.Empty;

    /// <summary>
    /// Minimum Withdrawal Amount
    /// </summary>
    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// Withdraw At Once
    /// </summary>
    [JsonProperty("wdAll")]
    public bool WithdrawAtOnce { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Contract Address
    /// </summary>
    [JsonProperty("ctAddr")]
    public string ContractAddress { get; set; } = string.Empty;

    /// <summary>
    /// Withdraw Tick Size
    /// </summary>
    [JsonProperty("wdTickSz")]
    public decimal WithdrawTickSize { get; set; }

    /// <summary>
    /// Need Tag
    /// </summary>
    [JsonProperty("needTag")]
    public bool NeedTag { get; set; }
}
