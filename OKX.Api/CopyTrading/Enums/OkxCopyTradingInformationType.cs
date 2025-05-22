namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Information Type
/// </summary>
public enum OkxCopyTradingInformationType : byte
{
    /// <summary>
    /// lead trading failed due to touch max position limitation
    /// </summary>
    [Map("1")]
    FailedDueToTouchMaximumPositionLimitation = 1,

    /// <summary>
    /// lead trading failed due to touch the maximum daily number of lead trading
    /// </summary>
    [Map("2")]
    FailedDueToTouchMaximumDailyNumberOfLeadTrading = 2,

    /// <summary>
    /// lead trading failed due to your USDT equity less than the minimum USDT equity of lead trading
    /// </summary>
    [Map("3")]
    FailedDueToUsdtEquityLessThanTheMinimumUsdtEquityOfLeadTrading = 3
}