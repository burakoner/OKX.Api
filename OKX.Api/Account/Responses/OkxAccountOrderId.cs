namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Order
/// </summary>
internal record OkxAccountOrderId
{
    /// <summary>
    /// Borrowing order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long? Data { get; set; }
}
