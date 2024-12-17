namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Limit
/// </summary>
public record OkxAccountFixedLoanBorrowingLimit
{
    /// <summary>
    /// (Master account and sub-accounts) Total borrow limit, unit in USD
    /// </summary>
    [JsonProperty("totalBorrowLmt")]
    public decimal TotalBorrowLimit { get; set; }

    /// <summary>
    /// (Master account and sub-accounts) Total available amount to borrow, unit in USD
    /// </summary>
    [JsonProperty("totalAvailBorrow")]
    public decimal TotalAvailableAmountToBorrow { get; set; }

    /// <summary>
    /// (Current account) Borrowed amount, unit in USD
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal BorrowedAmount { get; set; }

    /// <summary>
    /// (Current account) Used amount, unit in USD
    /// </summary>
    [JsonProperty("used")]
    public decimal UsedAmount { get; set; }

    /// <summary>
    /// (Current account) Available amount to repay, unit in USD
    /// </summary>
    [JsonProperty("availRepay")]
    public decimal AvailableRepay { get; set; }

    /// <summary>
    /// Borrow limit info in details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxAccountFixedLoanBorrowLimitDetails> Details { get; set; } = [];

    /// <summary>
    /// Data return time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data return time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OKX Fixed Loan Borrow Limit Details
/// </summary>
public record OkxAccountFixedLoanBorrowLimitDetails
{
    /// <summary>
    /// Borrowing currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Used amount of current currency
    /// </summary>
    [JsonProperty("used")]
    public decimal UsedAmount { get; set; }

    /// <summary>
    /// Borrowed amount of current currency
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal BorrowedAmount { get; set; }

    /// <summary>
    /// Available amount to borrow of current currency
    /// </summary>
    [JsonProperty("availBorrow")]
    public decimal AvailableAmountToBorrow { get; set; }

    /// <summary>
    /// Minimum borrow amount
    /// </summary>
    [JsonProperty("minBorrow")]
    public decimal MinimumBorrowAmount { get; set; }

    /// <summary>
    /// Borrowing term, e.g. 30D: 30 Days
    /// </summary>
    [JsonProperty("term")]
    public string Term { get; set; } = string.Empty;
}
