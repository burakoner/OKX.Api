namespace OKX.Api.Account;

/// <summary>
/// OKX Fixed Loan Borrow Order
/// </summary>
internal record OkxAccountOrderIdContainer
{
    /// <summary>
    /// Borrowing order ID
    /// </summary>
    [JsonProperty("ordId")]
    public long? Payload { get; set; }
}
