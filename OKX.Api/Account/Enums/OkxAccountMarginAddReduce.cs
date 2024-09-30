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
    Add = 1,

    /// <summary>
    /// Reduce
    /// </summary>
    [Map("reduce")]
    Reduce = 2,
}