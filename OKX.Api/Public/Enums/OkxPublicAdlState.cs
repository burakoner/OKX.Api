namespace OKX.Api.Public;

/// <summary>
/// OKX Public ADL State
/// </summary>
public enum OkxPublicAdlState : byte
{
    /// <summary>
    /// Normal. Retained for historical payload compatibility; OKX no longer pushes this state.
    /// </summary>
    [Obsolete("OKX no longer pushes ADL warning data in the normal state.")]
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
