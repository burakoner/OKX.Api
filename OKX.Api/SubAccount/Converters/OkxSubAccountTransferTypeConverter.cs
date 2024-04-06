using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Converters;

public class OkxSubAccountTransferTypeConverter(bool quotes) : BaseConverter<OkxSubAccountTransferType>(quotes)
{
    public OkxSubAccountTransferTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSubAccountTransferType, string>> Mapping =>
    [
        new(OkxSubAccountTransferType.FromMasterAccountToSubAccout, "0"),
        new(OkxSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    ];
}