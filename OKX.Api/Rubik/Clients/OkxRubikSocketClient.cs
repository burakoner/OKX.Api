namespace OKX.Api.Rubik.Clients;

/// <summary>
/// OKX WebSocket Api Trading Statistics Client
/// </summary>
public class OkxRubikSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }

    internal OkxRubikSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

}