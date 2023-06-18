using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Converters;
public class DepositAddressTypeConverter : BaseConverter<OkxDepositAddressType>
{
    public DepositAddressTypeConverter(): this(true) { }
    public DepositAddressTypeConverter(bool quotes) : base(quotes) { }
    protected override List<KeyValuePair<OkxDepositAddressType, string>> Mapping => new()
    {
            new KeyValuePair<OkxDepositAddressType, string>(OkxDepositAddressType.Regular, "1"),
            new KeyValuePair<OkxDepositAddressType, string>(OkxDepositAddressType.SegWit, "2"),

    };
}
