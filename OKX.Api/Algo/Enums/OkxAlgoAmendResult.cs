namespace OKX.Api.Algo;

/// <summary>
/// Algo order amendment result reported by the WebSocket channel.
/// </summary>
public enum OkxAlgoAmendResult : sbyte
{
    /// <summary>
    /// Amendment failed.
    /// </summary>
    [Map("-1")]
    Failure = -1,

    /// <summary>
    /// Amendment succeeded.
    /// </summary>
    [Map("0")]
    Success = 0,
}
