namespace OKX.Api.Base;

/// <summary>
/// Result of an explicit unsubscribe-with-confirmation operation, not a server wire response.
/// </summary>
public sealed record OkxSocketUnsubscribeResult
{
    /// <summary>
    /// Local SDK subscription ID.
    /// </summary>
    public int SubscriptionId { get; }

    /// <summary>
    /// The SDK marked this subscription closed and removed it from this connection's subscription registry.
    /// This does not prove remote removal or completion of callbacks that were already being dispatched.
    /// </summary>
    public bool LocalClosed { get; }

    /// <summary>
    /// Every requested distinct unsubscribe argument received a matching successful acknowledgement.
    /// False means this operation did not obtain complete server confirmation, not that all remote filters remain active.
    /// </summary>
    public bool ServerConfirmed { get; }

    internal OkxSocketUnsubscribeResult(int subscriptionId, bool localClosed, bool serverConfirmed)
    {
        SubscriptionId = subscriptionId;
        LocalClosed = localClosed;
        ServerConfirmed = serverConfirmed;
    }
}
