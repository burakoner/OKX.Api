﻿using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Models;

internal class OkxSocketRequest
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

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

    public OkxSocketRequest(string id, OkxSocketOperation op, IEnumerable<OkxSocketRequestArgument> args)
    {
        RequestId = id;
        Operation = op;
        Arguments = args.ToList();
    }
}

internal class OkxSocketRequest<T>
{
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

    [JsonProperty("op"), JsonConverter(typeof(OkxSocketOperationConverter))]
    public OkxSocketOperation Operation { get; set; }

    [JsonProperty("args")]
    public List<T> Arguments { get; set; }

    public OkxSocketRequest(OkxSocketOperation op, params T[] args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkxSocketRequest(OkxSocketOperation op, IEnumerable<T> args)
    {
        Operation = op;
        Arguments = args.ToList();
    }

    public OkxSocketRequest(string id, OkxSocketOperation op, IEnumerable<T> args)
    {
        RequestId = id;
        Operation = op;
        Arguments = args.ToList();
    }
}

internal class OkxSocketRequestArgument
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("instFamily", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentFamily { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string InstrumentId { get; set; }

    [JsonProperty("instType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OkxInstrumentTypeConverter))]
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

internal class OkxSocketAuthRequest
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

internal class OkxSocketAuthRequestArgument
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

internal enum OkxSocketOperation
{
    Login,
    Subscribe,
    Unsubscribe,
    
    Order,
    AmendOrder,
    CancelOrder,

    BatchOrders,
    BatchAmendOrders,
    BatchCancelOrders,

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
