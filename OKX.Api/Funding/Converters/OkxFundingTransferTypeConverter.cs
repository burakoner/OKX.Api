using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingTransferTypeConverter(bool quotes) : BaseConverter<OkxFundingTransferType>(quotes)
{
    public OkxFundingTransferTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingTransferType, string>> Mapping =>
    [
        new(OkxFundingTransferType.TransferWithinAccount, "0"),
        new(OkxFundingTransferType.MasterAccountToSubAccount, "1"),
        new(OkxFundingTransferType.SubAccountToMasterAccount_MasterApiKey, "2"),
        new(OkxFundingTransferType.SubAccountToMasterAccount_SubAccountApiKey, "3"),
        new(OkxFundingTransferType.SubAccountToSubAccount, "4"),
    ];
}