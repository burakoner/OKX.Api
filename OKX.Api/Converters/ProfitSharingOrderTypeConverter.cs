namespace OKX.Api.Converters;

internal class ProfitSharingOrderTypeConverter : BaseConverter<OkxProfitSharingOrderType>
{
    public ProfitSharingOrderTypeConverter() : this(true) { }
    public ProfitSharingOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxProfitSharingOrderType, string>> Mapping => new()
    {
        new KeyValuePair<OkxProfitSharingOrderType, string>(OkxProfitSharingOrderType.NormalOrder, "0"),
        new KeyValuePair<OkxProfitSharingOrderType, string>(OkxProfitSharingOrderType.CopyOrderWithoutProfitSharing, "1"),
        new KeyValuePair<OkxProfitSharingOrderType, string>(OkxProfitSharingOrderType.CopyOrderWithProfitSharing, "2"),
        new KeyValuePair<OkxProfitSharingOrderType, string>(OkxProfitSharingOrderType.LeadOrder, "3"),
    };
}