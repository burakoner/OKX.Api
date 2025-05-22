namespace OKX.Api.Account;

/// <summary>
/// Okx Isolated Margin Mode
/// </summary>
public enum OkxAccountIsolatedMarginMode : byte
{
    /// <summary>
    /// AutoTransfer
    /// </summary>
    [Map("automatic")]
    AutoTransfer = 1,

    /// <summary>
    /// ManualTransfer
    /// </summary>
    [Map("autonomy")]
    ManualTransfer = 2,

    /// <summary>
    /// QuickMarginMode
    /// </summary>
    [Map("quick_margin")]
    QuickMarginMode = 3,

    /// <summary>
    /// QuickMarginMode
    /// </summary>
    [Map("auto_transfers_ccy")]
    AutoTransfersCurrency = 4,
}