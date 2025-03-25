namespace OKX.Api.Funding;

/// <summary>
/// OKX WebSocket Api Funding Account Client
/// </summary>
public class OkxFundingSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    /// <summary>
    /// A push notification is triggered when a deposit is initiated or the deposit status changes.
    /// Supports subscriptions for accounts
    /// 
    /// If it is a master account subscription, you can receive the push of the deposit info of both the master account and the sub-account.
    /// If it is a sub-account subscription, only the push of sub-account deposit info you can receive.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToDepositsAsync(
        Action<OkxFundingDepositUpdate> onData,
        string? currency = null,
        CancellationToken ct = default)
    => await SubscribeToDepositsAsync(onData, [currency], ct).ConfigureAwait(false);

    /// <summary>
    /// A push notification is triggered when a deposit is initiated or the deposit status changes.
    /// Supports subscriptions for accounts
    /// 
    /// If it is a master account subscription, you can receive the push of the deposit info of both the master account and the sub-account.
    /// If it is a sub-account subscription, only the push of sub-account deposit info you can receive.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToDepositsAsync(
        Action<OkxFundingDepositUpdate> onData,
        IEnumerable<string?>? currencies = null,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxFundingDepositUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        if (currencies is not null) foreach (var currency in currencies) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "deposit-info",
            Currency = currency,
        });
        else arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "deposit-info",
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// A push notification is triggered when a withdrawal is initiated or the withdrawal status changes.
    /// Supports subscriptions for accounts
    /// 
    /// If it is a master account subscription, you can receive the push of the withdrawal info of both the master account and the sub-account.
    /// If it is a sub-account subscription, only the push of sub-account withdrawal info you can receive.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToWithdrawalsAsync(
        Action<OkxFundingWithdrawalUpdate> onData,
        string? currency = null,
        CancellationToken ct = default)
    => await SubscribeToWithdrawalsAsync(onData, [currency], ct).ConfigureAwait(false);

    /// <summary>
    /// A push notification is triggered when a withdrawal is initiated or the withdrawal status changes.
    /// Supports subscriptions for accounts
    /// 
    /// If it is a master account subscription, you can receive the push of the withdrawal info of both the master account and the sub-account.
    /// If it is a sub-account subscription, only the push of sub-account withdrawal info you can receive.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToWithdrawalsAsync(
        Action<OkxFundingWithdrawalUpdate> onData,
        IEnumerable<string?>? currencies = null,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxFundingWithdrawalUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        if (currencies is not null) foreach (var currency in currencies) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "withdrawal-info",
            Currency = currency,
        });
        else arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "withdrawal-info",
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }
}