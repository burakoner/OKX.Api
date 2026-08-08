namespace OKX.Api.Public;

/// <summary>
/// Event contract settlement method.
/// </summary>
public enum OkxPublicEventContractSettlementMethod : byte
{
    /// <summary>
    /// Price up/down.
    /// </summary>
    [Map("price_up_down")]
    PriceUpDown = 1,

    /// <summary>
    /// Price above.
    /// </summary>
    [Map("price_above")]
    PriceAbove = 2,

    /// <summary>
    /// Settles when the price touches the strike level.
    /// </summary>
    [Map("hit")]
    Hit = 3,

    /// <summary>
    /// Settles when the price is within the documented strike range.
    /// </summary>
    [Map("between")]
    Between = 4,
}
