namespace OKX.Api.Account;

/// <summary>
/// Account feature supported by the activation endpoint.
/// </summary>
public enum OkxAccountFeature : byte
{
    /// <summary>
    /// USDC order book trading. Activation is shared by the master account and all its sub-accounts.
    /// </summary>
    [Map("1")]
    UsdcOrderBookTrading = 1,
}
