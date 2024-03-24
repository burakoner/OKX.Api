namespace OKX.Api.Common.Models;

public class OkxSocketPingPong
{
    public DateTime PingTime { get; set; }
    public DateTime PongTime { get; set; }
    public string PongMessage { get; set; }
    public TimeSpan Latency { get; set; }
}
