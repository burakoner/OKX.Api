namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Kind
/// </summary>
public enum OkxAlgoOrderKind
{
    /// <summary>
    /// Condition
    /// </summary>
    [Map("condition")]
    Condition,

    /// <summary>
    /// Limit
    /// </summary>
    [Map("limit")]
    Limit,
}