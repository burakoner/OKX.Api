namespace OKX.Api.Trade;

/// <summary>
/// Source of an order amendment reported by the orders WebSocket channel.
/// </summary>
public enum OkxTradeOrderAmendSource : byte
{
    /// <summary>
    /// Order amended by the user.
    /// </summary>
    [Map("1")]
    User = 1,

    /// <summary>
    /// Order amended by the user, with quantity overridden by the reduce-only system.
    /// </summary>
    [Map("2")]
    UserWithReduceOnlyQuantityOverride = 2,

    /// <summary>
    /// New order quantity overridden by the reduce-only system.
    /// Retained for historical order payload compatibility.
    /// </summary>
    [Obsolete("OKX no longer documents amendSource=3 for current orders channel updates.")]
    [Map("3")]
    NewOrderWithReduceOnlyQuantityOverride = 3,

    /// <summary>
    /// Order amended by the reduce-only system because of other pending orders.
    /// </summary>
    [Map("4")]
    SystemReduceOnly = 4,

    /// <summary>
    /// Option order amended because px, pxVol, or pxUsd conversion changed.
    /// </summary>
    [Map("5")]
    OptionPriceConversion = 5,

    /// <summary>
    /// RPI order price rounded by the system to satisfy the maker spacing rule.
    /// </summary>
    [Map("6")]
    RpiPriceRounding = 6,
}
