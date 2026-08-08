namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate statistics window.
/// </summary>
public enum OkxAffiliatePeriodType : byte
{
    /// <summary>Last seven days.</summary>
    [Map("last_7d")]
    Last7Days = 0,

    /// <summary>Last thirty days.</summary>
    [Map("last_30d")]
    Last30Days = 1,

    /// <summary>Current calendar month.</summary>
    [Map("this_month")]
    ThisMonth = 2,

    /// <summary>Previous calendar month.</summary>
    [Map("last_month")]
    LastMonth = 3,

    /// <summary>Lifetime statistics.</summary>
    [Map("total")]
    Total = 4,

    /// <summary>Current day.</summary>
    [Map("today")]
    Today = 5,

    /// <summary>Current week.</summary>
    [Map("this_week")]
    ThisWeek = 6,

    /// <summary>Custom inclusive time range.</summary>
    [Map("custom")]
    Custom = 7,
}
