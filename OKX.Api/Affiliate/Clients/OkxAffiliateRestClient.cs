namespace OKX.Api.Affiliate;

/// <summary>
/// OKX Rest Api Affiliate Client
/// </summary>
public class OkxAffiliateRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AffiliateInviteeDetail = "api/v5/affiliate/invitee/detail";
    private const string v5UsersPartnerIfRebate = "api/v5/users/partner/if-rebate";

    /// <summary>
    /// Get the invitee's detail
    /// </summary>
    /// <param name="uid">UID of the invitee. Only applicable to the UID of invitee master account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAffiliateInvitee>> GetInviteeAsync(long uid, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uid", uid.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAffiliateInvitee>(GetUri(v5AffiliateInviteeDetail), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// This endpoint will be offline soon, please use Get the invitee's detail.
    /// It is used to get the user's affiliate rebate information for affiliate.
    /// </summary>
    /// <param name="apiKey">The user's API key. Only applicable to the API key of invitee master account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAffiliateRebateInformation>> GetRebateInformationAsync(string apiKey, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "apiKey", apiKey },
        };

        return ProcessOneRequestAsync<OkxAffiliateRebateInformation>(GetUri(v5UsersPartnerIfRebate), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}