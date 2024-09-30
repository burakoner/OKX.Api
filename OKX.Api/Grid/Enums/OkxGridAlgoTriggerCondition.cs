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
    CrossUp = 1,

    /// <summary>
    /// CrossDown
    /// </summary>
    [Map("cross_down")]
    CrossDown = 2,

    /// <summary>
    /// Above
    /// </summary>
    [Map("above")]
    Above = 3,

    /// <summary>
    /// Below
    /// </summary>
    [Map("below")]
    Below = 4,

    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross = 5
}
