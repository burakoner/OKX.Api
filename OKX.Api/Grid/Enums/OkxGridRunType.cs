namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Run Type
/// </summary>
public enum OkxGridRunType : byte
{
    /// <summary>
    /// Arithmetic
    /// </summary>
    [Map("1")]
    Arithmetic = 1,

    /// <summary>
    /// Geometric
    /// </summary>
    [Map("2")]
    Geometric = 2,
}