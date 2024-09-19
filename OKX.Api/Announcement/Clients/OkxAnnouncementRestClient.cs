namespace OKX.Api.Announcement.Clients;

/// <summary>
/// OKX Rest Api Announcement Client
/// </summary>
public class OkxAnnouncementRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5SupportAnnouncements = "api/v5/support/announcements";
    private const string v5SupportAnnouncementTypes = "api/v5/support/announcement-types";

}
