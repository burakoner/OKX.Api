namespace OKX.Api.Base.Models;

/// <summary>
/// OKX Socket Request
/// </summary>
public class OkxSocketRequest
{
    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

    /// <summary>
    /// Operation
    /// </summary>
    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("args")]
    public List<OkxSocketRequestArgument> Arguments { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(OkxSocketOperation op, params OkxSocketRequestArgument[] args)
    {
        Operation = op;
        Arguments = [.. args];
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(OkxSocketOperation op, IEnumerable<OkxSocketRequestArgument> args)
    {
        Operation = op;
        Arguments = [.. args];
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(string id, OkxSocketOperation op, IEnumerable<OkxSocketRequestArgument> args)
    {
        RequestId = id;
        Operation = op;
        Arguments = [.. args];
    }
}

/// <summary>
/// OKX Socket Request
/// </summary>
/// <typeparam name="T"></typeparam>
public class OkxSocketRequest<T>
{
    /// <summary>
    /// Request Id
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

    /// <summary>
    /// Operation
    /// </summary>
    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("args")]
    public List<T> Arguments { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(OkxSocketOperation op, params T[] args)
    {
        Operation = op;
        Arguments = [.. args];
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(OkxSocketOperation op, IEnumerable<T> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketRequest(string id, OkxSocketOperation op, IEnumerable<T> args)
    {
        RequestId = id;
        Operation = op;
        Arguments = args.ToList();
    }
}

/// <summary>
/// OKX Socket Request Argument
/// </summary>
public class OkxSocketRequestArgument
{
    /// <summary>
    /// Channel
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType? InstrumentType { get; set; }

    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoOrderId { get; set; }
}

/// <summary>
/// OKX Socket Symbol Request
/// </summary>
public class OkxSocketSymbolRequest
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Family
    /// </summary>
    public string InstrumentFamily { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    public string InstrumentId { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public OkxSocketSymbolRequest() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type"></param>
    /// <param name="family"></param>
    /// <param name="id"></param>
    public OkxSocketSymbolRequest(OkxInstrumentType type, string family, string id)
    {
        InstrumentType = type;
        InstrumentFamily = family;
        InstrumentId = id;
    }
}

/// <summary>
/// OKX Socket Auth Request
/// </summary>
public class OkxSocketAuthRequest
{
    /// <summary>
    /// Operation
    /// </summary>
    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("args")]
    public List<OkxSocketAuthRequestArgument> Arguments { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public OkxSocketAuthRequest()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketAuthRequest(OkxSocketOperation op, params OkxSocketAuthRequestArgument[] args)
    {
        Operation = op;
        Arguments = [.. args];
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="op"></param>
    /// <param name="args"></param>
    public OkxSocketAuthRequest(OkxSocketOperation op, IEnumerable<OkxSocketAuthRequestArgument> args)
    {
        Operation = op;
        Arguments = [.. args];
    }
}

/// <summary>
/// OKX Socket Auth Request Argument
/// </summary>
public class OkxSocketAuthRequestArgument
{
    /// <summary>
    /// API Key
    /// </summary>
    [JsonProperty("apiKey", NullValueHandling = NullValueHandling.Ignore)]
    public string ApiKey { get; set; }

    /// <summary>
    /// Passphrase
    /// </summary>
    [JsonProperty("passphrase", NullValueHandling = NullValueHandling.Ignore)]
    public string Passphrase { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public string Timestamp { get; set; }

    /// <summary>
    /// Signature
    /// </summary>
    [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
    public string Signature { get; set; }
}

/// <summary>
/// OKX Socket Operation
/// </summary>
public enum OkxSocketOperation
{
    /// <summary>
    /// Login
    /// </summary>
    Login,

    /// <summary>
    /// Subscribe
    /// </summary>
    Subscribe,

    /// <summary>
    /// Unsubscribe
    /// </summary>
    Unsubscribe,

    /// <summary>
    /// Place Order
    /// </summary>
    Order,

    /// <summary>
    /// Amend Order
    /// </summary>
    AmendOrder,

    /// <summary>
    /// Cancel Order
    /// </summary>
    CancelOrder,

    /// <summary>
    /// Batch Place Orders
    /// </summary>
    BatchOrders,

    /// <summary>
    /// Batch Amend Orders
    /// </summary>
    BatchAmendOrders,

    /// <summary>
    /// Batch Cancel Orders
    /// </summary>
    BatchCancelOrders,

    /// <summary>
    /// Mass Cancel
    /// </summary>
    MassCancel,
}

internal class OkxSocketOperationConverter : BaseConverter<OkxSocketOperation>
{
    public OkxSocketOperationConverter() : this(true) { }
    public OkxSocketOperationConverter(bool quotes) : base(quotes) { }
    protected override List<KeyValuePair<OkxSocketOperation, string>> Mapping =>
    [
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Login, "login"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Subscribe, "subscribe"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Unsubscribe, "unsubscribe"),

        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.Order, "order"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.AmendOrder, "amend-order"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.CancelOrder, "cancel-order"),

        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.BatchOrders, "batch-orders"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.BatchAmendOrders, "batch-amend-orders"),
        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.BatchCancelOrders, "batch-cancel-orders"),

        new KeyValuePair<OkxSocketOperation, string>(OkxSocketOperation.MassCancel, "mass-cancel"),
    ];
}
