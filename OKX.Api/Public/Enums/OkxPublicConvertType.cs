namespace OKX.Api.Public;

/// <summary>
/// OKX Convert Type
/// </summary>
public enum OkxPublicConvertType : byte
{
    /// <summary>
    /// CurrencyToContract
    /// </summary>
    [Map("1")]
    CurrencyToContract = 1,

    /// <summary>
    /// ContractToCurrency
    /// </summary>
    [Map("2")]
    ContractToCurrency = 2,
}