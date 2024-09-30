namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Destination
/// </summary>
public enum OkxFundingWithdrawalDestination
{
    /// <summary>
    /// OKX
    /// </summary>
    [Map("3")]
    OKX,

    /// <summary>
    /// DigitalCurrencyAddress
    /// </summary>
    [Map("4")]
    DigitalCurrencyAddress,
}