namespace OKX.Api.SubAccount;

internal class OkxSubAccountTransferTypeConverter(bool quotes) : BaseConverter<OkxSubAccountTransferType>(quotes)
{
    public OkxSubAccountTransferTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSubAccountTransferType, string>> Mapping =>
    [
        new(OkxSubAccountTransferType.FromMasterAccountToSubAccout, "0"),
        new(OkxSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    ];
}