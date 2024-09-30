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
    OKX = 3,

    /// <summary>
    /// DigitalCurrencyAddress
    /// </summary>
    [Map("4")]
    DigitalCurrencyAddress = 4,
}