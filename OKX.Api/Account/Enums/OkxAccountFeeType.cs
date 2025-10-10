namespace OKX.Api.Account;

/// <summary>
/// OKX Fee Type
/// </summary>
public enum OkxAccountFeeType : byte
{
    /// <summary>
    /// fee is charged in the currency you receive from the trade
    /// </summary>
    [Map("0")]
    ReceivedAsset = 0,

    /// <summary>
    /// fee is always charged in the quote currency of the trading pair
    /// </summary>
    [Map("1")]
    QuoteAsset = 1,
}