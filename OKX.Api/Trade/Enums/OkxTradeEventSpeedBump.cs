namespace OKX.Api.Trade;

/// <summary>
/// Event contract speed bump flag.
/// </summary>
public enum OkxTradeEventSpeedBump : byte
{
    /// <summary>
    /// Enable speed bump.
    /// </summary>
    [Map("1")]
    Enabled = 1,
}
