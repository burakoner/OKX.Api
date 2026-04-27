namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Direction
/// </summary>
public enum OkxDcaDirection : byte
{
    /// <summary>
    /// Long
    /// </summary>
    [Map("long")]
    Long = 1,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short = 2,
}
