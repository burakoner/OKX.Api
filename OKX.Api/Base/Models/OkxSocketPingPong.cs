namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Ping Pong
/// </summary>
public class OkxSocketPingPong
{
    /// <summary>
    /// Ping Time
    /// </summary>
    public DateTime PingTime { get; set; }

    /// <summary>
    /// Pong Time
    /// </summary>
    public DateTime PongTime { get; set; }

    /// <summary>
    /// Pong Message
    /// </summary>
    public string PongMessage { get; set; }

    /// <summary>
    /// Latency
    /// </summary>
    public TimeSpan Latency { get; set; }
}
