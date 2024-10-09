namespace OKX.Api.Block;

/// <summary>
/// OKX WebSocket Api Block Trading Client
/// </summary>
public class OkxBlockSocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient _ { get; } = root;

    // TODO: Rfqs channel
    // TODO: Quotes channel
    // TODO: Structure block trades channel

    // TODO: Public structure block trades channel
    // TODO: Public block trades channel
    // TODO: Block tickers channel

}