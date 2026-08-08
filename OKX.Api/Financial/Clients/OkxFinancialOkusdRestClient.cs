namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest Api OKUSD Client
/// </summary>
public class OkxFinancialOkusdRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Retrieve the remaining master-account-level daily OKUSD subscription and redemption limits.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialOkusdLimits>> GetLimitsAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxFinancialOkusdLimits>(GetUri("api/v5/finance/okusd/limits"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Subscribe USDT to receive OKUSD at a 1:1 rate with no subscription fee.
    /// Reusing the same client order ID returns the original order without executing another subscription.
    /// </summary>
    /// <param name="amount">USDT amount. Minimum 1 and maximum 8 decimal places.</param>
    /// <param name="clientOrderId">Unique client order ID per UID. Up to 32 letters, digits, hyphens, or underscores.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialOkusdSubscription>> SubscribeAsync(
        decimal amount,
        string clientOrderId,
        CancellationToken ct = default)
    {
        ValidateAmount(amount);
        ValidateClientOrderId(clientOrderId);

        var parameters = new ParameterCollection
        {
            { "amt", FormatAmount(amount) },
            { "clOrdId", clientOrderId },
        };

        return ProcessOneRequestAsync<OkxFinancialOkusdSubscription>(GetUri("api/v5/finance/okusd/subscribe"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Redeem OKUSD to USDT using fast or standard settlement.
    /// Reusing the same client order ID returns the original order without executing another redemption.
    /// </summary>
    /// <param name="amount">OKUSD amount. Minimum 1 and maximum 8 decimal places.</param>
    /// <param name="redemptionType">Redemption type</param>
    /// <param name="clientOrderId">Unique client order ID per UID. Up to 32 letters, digits, hyphens, or underscores.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialOkusdRedemption>> RedeemAsync(
        decimal amount,
        OkxFinancialOkusdRedemptionType redemptionType,
        string clientOrderId,
        CancellationToken ct = default)
    {
        ValidateAmount(amount);
        if (!Enum.IsDefined(typeof(OkxFinancialOkusdRedemptionType), redemptionType))
            throw new ArgumentOutOfRangeException(nameof(redemptionType), redemptionType, "Unknown OKUSD redemption type.");
        ValidateClientOrderId(clientOrderId);

        var parameters = new ParameterCollection
        {
            { "amt", FormatAmount(amount) },
            { "clOrdId", clientOrderId },
        };
        parameters.AddEnum("redeemType", redemptionType);

        return ProcessOneRequestAsync<OkxFinancialOkusdRedemption>(GetUri("api/v5/finance/okusd/redeem"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount < 1m)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, "OKUSD amount must be at least 1.");
        if (decimal.Round(amount, 8) != amount)
            throw new ArgumentException("OKUSD amount can have at most 8 decimal places.", nameof(amount));
    }

    private static string FormatAmount(decimal amount)
    {
        return amount.ToString("0.########", OkxConstants.OkxCultureInfo);
    }

    private static void ValidateClientOrderId(string clientOrderId)
    {
        if (clientOrderId is null)
            throw new ArgumentNullException(nameof(clientOrderId));
        if (!Regex.IsMatch(clientOrderId, "^[A-Za-z0-9_-]{1,32}$", RegexOptions.CultureInvariant))
            throw new ArgumentException("Client order ID must contain 1 to 32 letters, digits, hyphens, or underscores.", nameof(clientOrderId));
    }
}