using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridContractDirectionConverter : BaseConverter<OkxGridContractDirection>
{
    public OkxGridContractDirectionConverter() : this(true) { }
    public OkxGridContractDirectionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridContractDirection, string>> Mapping =>
    [
        new(OkxGridContractDirection.Long, "long"),
        new(OkxGridContractDirection.Short, "short"),
        new(OkxGridContractDirection.Neutral, "neutral"),
    ];
}