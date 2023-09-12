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
}

public class OkxSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("instFamily", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentFamily { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; }

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType? InstrumentType { get; set; }
}

public class OkxSocketSymbolRequest
{
    public OkxInstrumentType InstrumentType { get; set; }
    public string InstrumentFamily { get; set; }
    public string InstrumentId { get; set; }

    public OkxSocketSymbolRequest() { }
    public OkxSocketSymbolRequest(OkxInstrumentType type, string family, string id)
    {
        InstrumentType = type;
        InstrumentFamily = family;
        InstrumentId = id;
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
}

public enum OkxSocketOperation
{
    Subscribe,
    Unsubscribe,
    Login,
    Query,
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
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Query, "query"),
    };
}

public class OkxSocketArgument
{
    [JsonProperty("sprdId")]
    public string Symbol { get; set; }

    [JsonProperty("clOrdId", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientID { get; set; }

    [JsonProperty("side", NullValueHandling = NullValueHandling.Ignore)]
    public string Side { get; set; }

    [JsonProperty("ordType", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderType { get; set; }

    [JsonProperty("px", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Price { get; set; }

    [JsonProperty("sz", NullValueHandling = NullValueHandling.Ignore)]
    public decimal Size { get; set; }

    [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
    public string? Tags { get; set; }
}

public class OkxBaseSocketRequest
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("op")]
    public string Operation { get; set; }

    [JsonProperty("args")]
    public Dictionary<string,string> Arguments { get; set; }


    public OkxBaseSocketRequest(string op, Dictionary<string, string> args)
    {
        Operation = op;
        Arguments = args;
    }
}