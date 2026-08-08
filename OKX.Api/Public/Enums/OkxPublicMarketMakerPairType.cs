namespace OKX.Api.Public;

/// <summary>
/// Market Maker Program Pair Classification
/// </summary>
public enum OkxPublicMarketMakerPairType : byte
{
    /// <summary>
    /// High liquidity tier
    /// </summary>
    [Map("A", "Type A")]
    HighLiquidity = 1,

    /// <summary>
    /// Medium or low liquidity crypto assets
    /// </summary>
    [Map("B-Crypto", "Type B-Crypto")]
    Crypto = 2,

    /// <summary>
    /// Traditional finance instruments, applicable only to swaps
    /// </summary>
    [Map("B-TradFi", "Type B-TradFi")]
    TraditionalFinance = 3,
}