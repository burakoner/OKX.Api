namespace OKX.Api.Public;

/// <summary>
/// OKX Convert Type
/// </summary>
public enum OkxPublicConvertType
{
    /// <summary>
    /// CurrencyToContract
    /// </summary>
    [Map("1")]
    CurrencyToContract,

    /// <summary>
    /// ContractToCurrency
    /// </summary>
    [Map("2")]
    ContractToCurrency,
}