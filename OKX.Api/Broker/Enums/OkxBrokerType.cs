namespace OKX.Api.Broker;

/// <summary>
/// OKX Broker Type
/// </summary>
public enum OkxBrokerType : byte
{
    /// <summary>
    /// API Broker
    /// </summary>
    [Map("api")]
    Api = 1,

    /// <summary>
    /// OAuth Broker
    /// </summary>
    [Map("oauth")]
    OAuth = 2,
}
