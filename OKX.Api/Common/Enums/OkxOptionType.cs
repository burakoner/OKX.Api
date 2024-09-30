namespace OKX.Api.Common;

/// <summary>
/// OKX Option Type
/// </summary>
public enum OkxOptionType
{
    /// <summary>
    /// Call
    /// </summary>
    [Map("C")]
    Call = 1,

    /// <summary>
    /// Put
    /// </summary>
    [Map("P")]
    Put = 2,
}