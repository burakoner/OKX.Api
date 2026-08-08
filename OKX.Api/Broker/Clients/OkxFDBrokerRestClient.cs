namespace OKX.Api.Broker;

/// <summary>
/// OKX Rest Api Fully Disclosed Broker Client
/// </summary>
public class OkxFDBrokerRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    #region Fully Disclosed Broker API Endpoints

    /// <summary>
    /// Get download links for FD broker commission rebate details that have already been generated.
    /// Each request refreshes the returned links, which remain valid for two hours.
    /// </summary>
    /// <param name="allHistory">Whether to return all generated history. When false, begin and end are required.</param>
    /// <param name="begin">Begin date, inclusive.</param>
    /// <param name="end">End date, exclusive.</param>
    /// <param name="brokerType">Broker type. Required by OKX when the broker has multiple broker types.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxDownloadLink>>> GetDownloadLinksAsync(
        bool allHistory,
        DateTime? begin = null,
        DateTime? end = null,
        OkxBrokerType? brokerType = null,
        CancellationToken ct = default)
    {
        if (!allHistory && (!begin.HasValue || !end.HasValue))
            throw new ArgumentException("begin and end are required when allHistory is false.");

        var parameters = new ParameterCollection
        {
            { "type", allHistory ? "true" : "false" }
        };
        parameters.AddOptional("begin", begin?.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        parameters.AddOptional("end", end?.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        parameters.AddOptionalEnum("brokerType", brokerType);

        return ProcessListRequestAsync<OkxDownloadLink>(GetUri("api/v5/broker/fd/rebate-per-orders"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    // TODO: POST /api/v5/broker/fd/rebate-per-orders

    /// <summary>
    /// Get whether a user's API key can contribute an FD broker rebate and, if not, the reason why.
    /// </summary>
    /// <param name="apiKey">The user's API key.</param>
    /// <param name="brokerType">Broker type. Required by OKX when the broker has multiple broker types.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFDBrokerRebateInformation>> GetRebateInformationAsync(
        string apiKey,
        OkxBrokerType? brokerType = null,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key is required.", nameof(apiKey));

        var parameters = new ParameterCollection
        {
            { "apiKey", apiKey }
        };
        parameters.AddOptionalEnum("brokerType", brokerType);

        return ProcessOneRequestAsync<OkxFDBrokerRebateInformation>(GetUri("api/v5/broker/fd/if-rebate"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    #endregion
}
