namespace OKX.Api.Account;

/// <summary>
/// Okx Isolated Margin Mode
/// </summary>
public enum OkxAccountIsolatedMarginMode
{
    /// <summary>
    /// AutoTransfer
    /// </summary>
    [Map("automatic")]
    AutoTransfer,

    /// <summary>
    /// ManualTransfer
    /// </summary>
    [Map("autonomy")]
    ManualTransfer,

    /// <summary>
    /// QuickMarginMode
    /// </summary>
    [Map("quick_margin")]
    QuickMarginMode,
}