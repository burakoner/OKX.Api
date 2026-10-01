namespace OKX.Api.Public;

internal static class OkxPublicMarketDataHistoryDates
{
    internal static DateTime GetDate(long timestamp, OkxPublicMarketDataHistoryModule module)
    {
        var offset = module switch
        {
            OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook or
            OkxPublicMarketDataHistoryModule.FiveThousandLevelOrderBook or
            OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook => TimeSpan.Zero,
            OkxPublicMarketDataHistoryModule.TradeHistory or
            OkxPublicMarketDataHistoryModule.OneMinuteCandlestick or
            OkxPublicMarketDataHistoryModule.FundingRate or
            OkxPublicMarketDataHistoryModule.BorrowingRate => TimeSpan.FromHours(8),
            _ => throw new ArgumentOutOfRangeException(nameof(module), module, "Unsupported historical market data module."),
        };

        // A calendar date is not a UTC/local instant. DateTimeOffset.Date returns Kind.Unspecified.
        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).ToOffset(offset).Date;
    }
}
