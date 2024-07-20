using OKX.Api.Affiliate.Models;

namespace OKX.Api.Affiliate.Clients;

/// <summary>
/// OKX Rest Api Affiliate Client
/// </summary>
public class OkxAffiliateRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AffiliateInviteeDetail = "api/v5/affiliate/invitee/detail";
    
    public Task<RestCallResult<OkxAffiliateInvitee>> GetInviteeAsync(long uid, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "uid", uid.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAffiliateInvitee>(GetUri(v5AffiliateInviteeDetail), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}