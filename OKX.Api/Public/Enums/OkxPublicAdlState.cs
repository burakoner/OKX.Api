namespace OKX.Api.Public;

/// <summary>
/// OKX Public ADL State
/// </summary>
public enum OkxPublicAdlState : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("normal")]
    normal = 1,

    /// <summary>
    /// Warning
    /// </summary>
    [Map("warning")]
    warning = 2,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("adl")]
    ADL = 3,
}