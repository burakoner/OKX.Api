namespace OKX.Api.Enums;

/// <summary>
/// OKX Profit Sharing Order Type
/// </summary>
public enum OkxProfitSharingOrderType
{
    /// <summary>
    /// Normal order
    /// </summary>
    NormalOrder,

    /// <summary>
    /// Copy order without profit sharing
    /// </summary>
    CopyOrderWithoutProfitSharing,

    /// <summary>
    /// Copy order with profit sharing
    /// </summary>
    CopyOrderWithProfitSharing,

    /// <summary>
    /// Lead order
    /// </summary>
    LeadOrder,
}