namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg trigger action.
/// </summary>
public enum OkxAlgoSmartIcebergTriggerAction : byte
{
    /// <summary>
    /// Start the iceberg order.
    /// </summary>
    [Map("start")]
    Start = 1,
}
