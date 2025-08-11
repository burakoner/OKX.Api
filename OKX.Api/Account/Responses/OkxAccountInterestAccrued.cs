namespace OKX.Api.Account;

/// <summary>
/// OkxInterestAccrued
/// </summary>
public record OkxAccountInterestAccrued
{
    /// <summary>
    /// Loan type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountLoanType LoanType { get; set; }

    /// <summary>
    /// Loan currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID, e.g. BTC-USDT
    /// Only applicable to Market loans
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Interest rate (in hour)
    /// </summary>
    [JsonProperty("interestRate")]
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Liability
    /// </summary>
    [JsonProperty("liab")]
    public decimal Liability { get; set; }

    /// <summary>
    /// Total liability for current account
    /// </summary>
    [JsonProperty("totalLiab")]
    public decimal TotalLiability { get; set; }

    /// <summary>
    /// Interest-free liability for current account
    /// </summary>
    [JsonProperty("interestFreeLiab")]
    public decimal InterestFreeLiability { get; set; }

    /// <summary>
    /// Timestamp for interest accured, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Timestamp for interest accured
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
