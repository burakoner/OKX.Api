using OKX.Api.Status.Converters;
using OKX.Api.Status.Enums;
using OKX.Api.Status.Models;

namespace OKX.Api.Status.Clients;

/// <summary>
/// OKX Rest Api Status Client
/// </summary>
public class OkxStatusRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5SystemStatus = "api/v5/system/status";

    /// <summary>
    /// Get event status of system upgrade.
    /// Planned system maintenance that may result in short interruption (lasting less than 5 seconds) or websocket disconnection (users can immediately reconnect) will not be announced. 
    /// The maintenance will only be performed during times of low market volatility.
    /// </summary>
    /// <param name="state">System maintenance status</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAnnouncements>>> GetSystemUpgradeStatusAsync(
        OkxStatusMaintenanceState? state = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxStatusMaintenanceStateConverter(false)));

        return ProcessListRequestAsync<OkxAnnouncements>(GetUri(v5SystemStatus), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

}
