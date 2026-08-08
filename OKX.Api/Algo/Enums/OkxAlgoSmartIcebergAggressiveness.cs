namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg execution aggressiveness.
/// </summary>
public enum OkxAlgoSmartIcebergAggressiveness : byte
{
    /// <summary>
    /// Faster fill.
    /// </summary>
    [Map("radical")]
    Radical = 1,

    /// <summary>
    /// Faster fill with a better price.
    /// </summary>
    [Map("mid")]
    Mid = 2,

    /// <summary>
    /// Queue at the best bid or ask.
    /// </summary>
    [Map("conservative")]
    Conservative = 3,
}
