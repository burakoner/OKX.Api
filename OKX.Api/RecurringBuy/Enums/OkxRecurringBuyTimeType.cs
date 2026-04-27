namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX Recurring Buy Time Type
/// </summary>
public enum OkxRecurringBuyTimeType : byte
{
    /// <summary>
    /// Custom time
    /// </summary>
    [Map("1")]
    Custom = 1,

    /// <summary>
    /// Immediate trigger
    /// </summary>
    [Map("2")]
    Immediate = 2
}
