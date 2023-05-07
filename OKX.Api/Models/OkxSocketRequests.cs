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

namespace OKX.Api.Models;

public class OkxSocketRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OkxSocketRequestArgument> Arguments { get; set; }

    public OkxSocketRequest(OkxSocketOperation op, params OkxSocketRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkxSocketRequest(OkxSocketOperation op, IEnumerable<OkxSocketRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkxSocketRequest(OkxSocketOperation op, string channel)
    {
        Operation = op;
        Arguments = new List<OkxSocketRequestArgument>();
        Arguments.Add(new OkxSocketRequestArgument(channel));
    }

    public OkxSocketRequest(OkxSocketOperation op, string channel, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OkxSocketRequestArgument>();
        Arguments.Add(new OkxSocketRequestArgument(channel, instrumentId));
    }

    public OkxSocketRequest(OkxSocketOperation op, string channel, string underlying, string instrumentId)
    {
        Operation = op;
        Arguments = new List<OkxSocketRequestArgument>();
        Arguments.Add(new OkxSocketRequestArgument(channel, underlying, instrumentId));
    }

    public OkxSocketRequest(OkxSocketOperation op, string channel, OkxInstrumentType instrumentType)
    {
        Operation = op;
        Arguments = new List<OkxSocketRequestArgument>();
        Arguments.Add(new OkxSocketRequestArgument(channel, instrumentType));
    }

    public OkxSocketRequest(OkxSocketOperation op, string channel, OkxInstrumentType instrumentType, string underlying)
    {
        Operation = op;
        Arguments = new List<OkxSocketRequestArgument>();
        Arguments.Add(new OkxSocketRequestArgument(channel, instrumentType, underlying));
    }

}

public class OkxSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("uly", NullValueHandling = NullValueHandling.Ignore)]
    public string Underlying { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; }

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType? InstrumentType { get; set; }

    public OkxSocketRequestArgument()
    {
    }

    public OkxSocketRequestArgument(string channel)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
    }

    public OkxSocketRequestArgument(string channel, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OkxSocketRequestArgument(string channel, string underlying, string instrumentId)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
    }

    public OkxSocketRequestArgument(string channel, OkxInstrumentType? instrumentType)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (instrumentType != null) InstrumentType = instrumentType;
    }

    public OkxSocketRequestArgument(string channel, OkxInstrumentType? instrumentType, string underlying)
    {
        if (!string.IsNullOrEmpty(channel)) Channel = channel;
        if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
        if (instrumentType != null) InstrumentType = instrumentType;
    }
}

public class OkxSocketAuthRequest
{
    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<OkxSocketAuthRequestArgument> Arguments { get; set; }

    public OkxSocketAuthRequest()
    {
    }

    public OkxSocketAuthRequest(OkxSocketOperation op, params OkxSocketAuthRequestArgument[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkxSocketAuthRequest(OkxSocketOperation op, IEnumerable<OkxSocketAuthRequestArgument> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }
}

public class OkxSocketAuthRequestArgument
{
    [JsonProperty("apiKey", NullValueHandling = NullValueHandling.Ignore)]
    public string ApiKey { get; set; }

    [JsonProperty("passphrase", NullValueHandling = NullValueHandling.Ignore)]
    public string Passphrase { get; set; }

    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public string Timestamp { get; set; }

    [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
    public string Signature { get; set; }

    public OkxSocketAuthRequestArgument()
    {
    }
}

public enum OkxSocketOperation
{
    Subscribe,
    Unsubscribe,
    Login,
}

public class OkxSocketOperationConverter : BaseConverter<OkxSocketOperation>
{
    public OkxSocketOperationConverter() : this(true) { }
    public OkxSocketOperationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSocketOperation, string>> Mapping => new List<KeyValuePair<OkxSocketOperation, string>>
    {
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Subscribe, "subscribe"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Unsubscribe, "unsubscribe"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Login, "login"),
    };
}
