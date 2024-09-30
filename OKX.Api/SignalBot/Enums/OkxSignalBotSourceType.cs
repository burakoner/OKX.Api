namespace OKX.Api.SignalBot;

/// <summary>
/// Signal source type
/// </summary>
public enum OkxSignalBotSourceType
{
    /// <summary>
    /// Created by yourself
    /// </summary>
    [Map("1")]
    CreatedbyYourself = 1,

    /// <summary>
    /// Subscribe
    /// </summary>
    [Map("2")]
    Subscribe = 2,

    /// <summary>
    /// Free signal
    /// </summary>
    [Map("3")]
    FreeSignal = 3,
}
