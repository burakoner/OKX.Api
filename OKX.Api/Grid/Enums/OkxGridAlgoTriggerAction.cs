namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Trigger Action
/// </summary>
public enum OkxGridAlgoTriggerAction : byte
{
    /// <summary>
    /// Start
    /// </summary>
    [Map("start")]
    Start = 1,

    /// <summary>
    /// Stop
    /// </summary>
    [Map("stop")]
    Stop = 2,
}