using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxTransferTypeConverter(bool quotes) : BaseConverter<OkxTransferType>(quotes)
{
    public OkxTransferTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTransferType, string>> Mapping =>
    [
        new(OkxTransferType.TransferWithinAccount, "0"),
        new(OkxTransferType.MasterAccountToSubAccount, "1"),
        new(OkxTransferType.SubAccountToMasterAccount_MasterApiKey, "2"),
        new(OkxTransferType.SubAccountToMasterAccount_SubAccountApiKey, "3"),
        new(OkxTransferType.SubAccountToSubAccount, "4"),
    ];
}