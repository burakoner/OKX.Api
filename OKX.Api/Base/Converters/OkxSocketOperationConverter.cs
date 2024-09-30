namespace OKX.Api.Base;

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
