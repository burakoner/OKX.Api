using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxProfitSharingOrderTypeConverter(bool quotes) : BaseConverter<OkxProfitSharingOrderType>(quotes)
{
    public OkxProfitSharingOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxProfitSharingOrderType, string>> Mapping =>
    [
        new(OkxProfitSharingOrderType.NormalOrder, "0"),
        new(OkxProfitSharingOrderType.CopyOrderWithoutProfitSharing, "1"),
        new(OkxProfitSharingOrderType.CopyOrderWithProfitSharing, "2"),
        new(OkxProfitSharingOrderType.LeadOrder, "3"),
    ];
}