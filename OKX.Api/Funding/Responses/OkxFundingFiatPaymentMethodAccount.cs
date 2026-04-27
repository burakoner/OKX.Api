namespace OKX.Api.Funding;

/// <summary>
/// OKX fiat payment account
/// </summary>
public record OkxFundingFiatPaymentMethodAccount
{
    /// <summary>
    /// Payment account identifier.
    /// </summary>
    [JsonProperty("paymentAcctId")]
    public string PaymentAccountId { get; set; } = string.Empty;

    /// <summary>
    /// Masked or plain account number shown by OKX for the payment account.
    /// </summary>
    [JsonProperty("acctNum")]
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Recipient or account holder name.
    /// </summary>
    [JsonProperty("recipientName")]
    public string RecipientName { get; set; } = string.Empty;

    /// <summary>
    /// Bank or provider name.
    /// </summary>
    [JsonProperty("bankName")]
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// Bank or provider code.
    /// </summary>
    [JsonProperty("bankCode")]
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// Current account state reported by OKX.
    /// </summary>
    [JsonProperty("state")]
    public string State { get; set; } = string.Empty;
}
