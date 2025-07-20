namespace OKX.Api.Affiliate;

/// <summary>
/// OKX Rest Api Affiliate Client
/// </summary>
public class OkxAffiliateRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Get the invitee's detail
    /// </summary>
    /// <param name="uid">UID of the invitee. Only applicable to the UID of invitee master account</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAffiliateInvitee>> GetInviteeAsync(long uid, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "uid", uid.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAffiliateInvitee>(GetUri("api/v5/affiliate/invitee/detail"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
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
        var parameters = new ParameterCollection
        {
            { "apiKey", apiKey },
        };

        return ProcessOneRequestAsync<OkxAffiliateRebateInformation>(GetUri("api/v5/users/partner/if-rebate"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}