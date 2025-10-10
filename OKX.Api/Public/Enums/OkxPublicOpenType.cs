namespace OKX.Api.Public;

/// <summary>
/// Okx Public Open Type
/// </summary>
public enum OkxPublicOpenType : byte
{
    /// <summary>
    /// fix price opening
    /// </summary>
    [Map("fix_price")]
    fix_price = 1,

    /// <summary>
    /// pre-quote
    /// </summary>
    [Map("pre_quote")]
    pre_quote = 2,

    /// <summary>
    /// call auction
    /// </summary>
    [Map("call_auction")]
    call_auction = 3,
}