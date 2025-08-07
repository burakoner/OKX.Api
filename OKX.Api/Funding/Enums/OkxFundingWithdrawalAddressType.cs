namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Address Type
/// </summary>
public enum OkxFundingWithdrawalAddressType : byte
{
    /// <summary>
    /// wallet address, email, phone, or login account name
    /// </summary>
    [Map("1")]
    AddressInfo = 1,

    /// <summary>
    /// UID (only applicable for dest=3)
    /// </summary>
    [Map("2")]
    UID = 2,
}