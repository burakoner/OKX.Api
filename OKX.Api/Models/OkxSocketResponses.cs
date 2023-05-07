/* Unmerged change from project 'OKX.Api (netstandard2.0)'
Before:
namespace OKX.Api.Models.Core;
After:
using OKX;
using OKX.Api;
using OKX.Api.Models;
using OKX.Api.Models;
using OKX.Api.Models.Core;
*/

using OKX.Api.Models.MarketData;

namespace OKX.Api.Models;

public class OkxSocketResponse
{
    public bool Success
    {
        get
        {
            return
                string.IsNullOrEmpty(ErrorCode)
                || ErrorCode.Trim() == "0";
        }
    }

    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("code")]
    public string ErrorCode { get; set; }

    [JsonProperty("msg")]
    public string ErrorMessage { get; set; }
}

public class OkxSocketUpdateResponse<T> : OkxSocketResponse
{
    /*
    [JsonOptionalProperty]
    public T Data { get; set; } = default!;
    [JsonOptionalProperty, JsonProperty("data")]
    private T Tick { set => Data = value; get => Data; }
    */

    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}

public class OkxOrderBookUpdate
{
    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("data")]
    public IEnumerable<OkxOrderBook> Data { get; set; } = default!;
}
