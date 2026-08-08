namespace OKX.Api.Public;

/// <summary>
/// Historical market data query.
/// </summary>
public record OkxPublicMarketDataHistoryQueryRequest
{
    /// <summary>Data module.</summary>
    public OkxPublicMarketDataHistoryModule Module { get; set; }

    /// <summary>SPOT, FUTURES, SWAP, or OPTION.</summary>
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>Daily or monthly file aggregation.</summary>
    public OkxPublicDateAggregationType DateAggregationType { get; set; }

    /// <summary>One to ten comma-separated instrument IDs, or a supported ANY query. Required only for SPOT.</summary>
    public string? InstrumentIdList { get; set; }

    /// <summary>One to ten comma-separated instrument families, or a supported ANY query. Required for non-SPOT types.</summary>
    public string? InstrumentFamilyList { get; set; }

    /// <summary>Inclusive begin timestamp in Unix milliseconds.</summary>
    public long? Begin { get; set; }

    /// <summary>Inclusive end timestamp in Unix milliseconds.</summary>
    public long? End { get; set; }

    internal void Validate()
    {
        if (!Enum.IsDefined(typeof(OkxPublicMarketDataHistoryModule), Module))
            throw new ArgumentOutOfRangeException(nameof(Module), Module, "Unsupported historical market data module.");
        if (InstrumentType.IsNotIn(OkxInstrumentType.Spot, OkxInstrumentType.Futures, OkxInstrumentType.Swap, OkxInstrumentType.Option))
            throw new ArgumentOutOfRangeException(nameof(InstrumentType), InstrumentType, "Historical market data supports SPOT, FUTURES, SWAP, and OPTION only.");
        if (!Enum.IsDefined(typeof(OkxPublicDateAggregationType), DateAggregationType))
            throw new ArgumentOutOfRangeException(nameof(DateAggregationType), DateAggregationType, "Unsupported date aggregation type.");
        if (!Begin.HasValue || !End.HasValue)
            throw new ArgumentException("Begin and End are required.");

        var instrumentList = InstrumentType == OkxInstrumentType.Spot
            ? ValidateInstrumentList(InstrumentIdList, nameof(InstrumentIdList), maximumCount: 10)
            : ValidateInstrumentList(InstrumentFamilyList, nameof(InstrumentFamilyList), maximumCount: Module == OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook && InstrumentType == OkxInstrumentType.Option ? 1 : 10);

        if (InstrumentType == OkxInstrumentType.Spot && !string.IsNullOrWhiteSpace(InstrumentFamilyList))
            throw new ArgumentException("InstrumentFamilyList is not applicable to SPOT.", nameof(InstrumentFamilyList));
        if (InstrumentType != OkxInstrumentType.Spot && !string.IsNullOrWhiteSpace(InstrumentIdList))
            throw new ArgumentException("InstrumentIdList is only applicable to SPOT.", nameof(InstrumentIdList));

        var anyInstrument = instrumentList.Length == 1 && instrumentList[0] == "ANY";
        if (instrumentList.Contains("ANY") && !anyInstrument)
            throw new ArgumentException("ANY cannot be combined with specific instruments.");
        if (anyInstrument && (DateAggregationType != OkxPublicDateAggregationType.Daily || Module.IsNotIn(
            OkxPublicMarketDataHistoryModule.TradeHistory,
            OkxPublicMarketDataHistoryModule.OneMinuteCandlestick,
            OkxPublicMarketDataHistoryModule.FundingRate,
            OkxPublicMarketDataHistoryModule.BorrowingRate)))
            throw new ArgumentException("ANY is supported only for daily modules 1, 2, 3, and 11.");
        if (Module == OkxPublicMarketDataHistoryModule.FundingRate &&
            DateAggregationType == OkxPublicDateAggregationType.Daily &&
            (InstrumentType == OkxInstrumentType.Spot || !anyInstrument))
            throw new ArgumentException("Daily funding-rate history requires instFamilyList=ANY.");
        if (Module == OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook && DateAggregationType == OkxPublicDateAggregationType.Monthly)
            throw new ArgumentException("Monthly aggregation is not supported for module 6.", nameof(DateAggregationType));

        ValidateDateRange(Begin.Value, End.Value);
    }

    private static string[] ValidateInstrumentList(string? value, string parameterName, int maximumCount)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("An instrument list is required.", parameterName);

        var values = value!.Split(',');
        if (values.Length > maximumCount)
            throw new ArgumentOutOfRangeException(parameterName, values.Length, $"The instrument list cannot contain more than {maximumCount} values.");
        if (values.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Instrument lists cannot contain empty values.", parameterName);
        return values;
    }

    private void ValidateDateRange(long begin, long end)
    {
        var timezone = Module.IsIn(
            OkxPublicMarketDataHistoryModule.FourHundredLevelOrderBook,
            OkxPublicMarketDataHistoryModule.FiveThousandLevelOrderBook,
            OkxPublicMarketDataHistoryModule.FiftyLevelOrderBook)
            ? TimeSpan.Zero
            : TimeSpan.FromHours(8);
        var beginDate = DateTimeOffset.FromUnixTimeMilliseconds(begin).ToOffset(timezone).Date;
        var endDate = DateTimeOffset.FromUnixTimeMilliseconds(end).ToOffset(timezone).Date;

        if (endDate < beginDate)
            throw new ArgumentException("End date cannot be before Begin date.", nameof(End));
        if (DateAggregationType == OkxPublicDateAggregationType.Daily && endDate >= beginDate.AddDays(10))
            throw new ArgumentOutOfRangeException(nameof(End), end, "Daily queries can contain at most 10 inclusive calendar days.");

        var monthDifference = (endDate.Year - beginDate.Year) * 12 + endDate.Month - beginDate.Month;
        if (DateAggregationType == OkxPublicDateAggregationType.Monthly && monthDifference >= 10)
            throw new ArgumentOutOfRangeException(nameof(End), end, "Monthly queries can contain at most 10 inclusive calendar months.");
    }
}
