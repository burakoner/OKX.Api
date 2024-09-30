namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Trigger Condition
/// </summary>
public enum OkxGridAlgoTriggerCondition
{
    /// <summary>
    /// CrossUp
    /// </summary>
    [Map("cross_up")]
    CrossUp,

    /// <summary>
    /// CrossDown
    /// </summary>
    [Map("cross_down")]
    CrossDown,

    /// <summary>
    /// Above
    /// </summary>
    [Map("above")]
    Above,

    /// <summary>
    /// Below
    /// </summary>
    [Map("below")]
    Below,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross
}
