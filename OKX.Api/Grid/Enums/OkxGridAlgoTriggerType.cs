namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Trigger Type
/// </summary>
public enum OkxGridAlgoTriggerType
{
    /// <summary>
    /// Auto
    /// </summary>
    [Map("auto")]
    Auto = 1,

    /// <summary>
    /// Manual
    /// </summary>
    [Map("manual")]
    Manual = 2,
}