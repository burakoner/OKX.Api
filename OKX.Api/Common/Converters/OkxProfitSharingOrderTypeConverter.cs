namespace OKX.Api.Common.Converters;

internal class OkxProfitSharingOrderTypeConverter : BaseConverter<OkxProfitSharingOrderType>
{
    public OkxProfitSharingOrderTypeConverter() : this(true) { }
    public OkxProfitSharingOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxProfitSharingOrderType, string>> Mapping =>
    [
        new(OkxProfitSharingOrderType.NormalOrder, "0"),
        new(OkxProfitSharingOrderType.CopyOrderWithoutProfitSharing, "1"),
        new(OkxProfitSharingOrderType.CopyOrderWithProfitSharing, "2"),
        new(OkxProfitSharingOrderType.LeadOrder, "3"),
    ];
}