namespace OKX.Api.Account;

/// <summary>
/// OKX Margin Add Reduce
/// </summary>
public enum OkxAccountMarginAddReduce
{
    /// <summary>
    /// Add
    /// </summary>
    [Map("add")]
    Add,

    /// <summary>
    /// Reduce
    /// </summary>
    [Map("reduce")]
    Reduce,
}