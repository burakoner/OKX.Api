namespace OKX.Api.Account.Enums;

/// <summary>
/// Okx Isolated Margin Mode
/// </summary>
public enum OkxAccountIsolatedMarginMode
{
    /// <summary>
    /// AutoTransfer
    /// </summary>
    AutoTransfer,

    /// <summary>
    /// ManualTransfer
    /// </summary>
    ManualTransfer,

    /// <summary>
    /// QuickMarginMode
    /// </summary>
    [Obsolete]
    QuickMarginMode,
}