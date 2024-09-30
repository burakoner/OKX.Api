namespace OKX.Api.Announcement;

/// <summary>
/// OKX Rest Api Announcement Client
/// </summary>
public class OkxAnnouncementRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5SupportAnnouncements = "api/v5/support/announcements";
    private const string v5SupportAnnouncementTypes = "api/v5/support/announcement-types";

    /// <summary>
    /// Authentication is required for this private endpoint.
    /// Get announcements, the response is sorted by pTime with the most recent first. The sort will not be affected if the announcement is updated. Every page has 20 records
    /// </summary>
    /// <param name="announcementType">Announcement type. Delivering the annType from "GET / Announcement types"
    /// Returning all when it is not posted</param>
    /// <param name="page">Page for pagination. The default is 1</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAnnouncements>> GetAnnouncementsAsync(string? announcementType = null, int? page = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("annType", announcementType);
        parameters.AddOptional("page", page?.ToOkxString());

        return ProcessOneRequestAsync<OkxAnnouncements>(GetUri(v5SupportAnnouncements), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Authentication is not required for this public endpoint. Get announcements types
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAnnouncementType>>> GetAnnouncementTypesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxAnnouncementType>(GetUri(v5SupportAnnouncementTypes), HttpMethod.Get, ct, signed: false);
    }
}
