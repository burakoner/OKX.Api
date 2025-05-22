namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX CopyTrading Instrument Id Type
/// </summary>
public enum OkxCopyTradingInstrumentIdType : byte
{
    /// <summary>
    /// custom: custom by instId which is required；
    /// </summary>
    [Map("custom")]
    Custom = 1,

    /// <summary>
    /// copy: Keep your contracts consistent with this trader by automatically adding or removing contracts when they do
    /// </summary>
    [Map("copy")]
    Copy = 2,
}