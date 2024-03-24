using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridContractDirectionConverter : BaseConverter<OkxGridContractDirection>
{
    public GridContractDirectionConverter() : this(true) { }
    public GridContractDirectionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridContractDirection, string>> Mapping => new List<KeyValuePair<OkxGridContractDirection, string>>
    {
        new KeyValuePair<OkxGridContractDirection, string>(OkxGridContractDirection.Long, "long"),
        new KeyValuePair<OkxGridContractDirection, string>(OkxGridContractDirection.Short, "short"),
        new KeyValuePair<OkxGridContractDirection, string>(OkxGridContractDirection.Neutral, "neutral"),
    };
}