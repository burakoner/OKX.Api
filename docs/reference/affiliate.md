# Affiliate

[Docs Home](../index.md) | [REST Reference](./index.md) | [Broker](./broker.md)

Official OKX docs: [Affiliate](https://www.okx.com/docs-v5/en/#affiliate)

## Overview

`api.Affiliate` exposes private affiliate performance, invitee, link, and sub-affiliate reporting. It requires credentials.

## Example Calls

```csharp
var summary = await api.Affiliate.GetPerformanceSummaryAsync(
    new OkxAffiliatePerformanceSummaryRequest
    {
        PeriodType = OkxAffiliatePeriodType.Last30Days
    });

var invitees = await api.Affiliate.GetInviteesAsync(
    new OkxAffiliateInviteeListRequest
    {
        Page = 1,
        Limit = 100,
        UserIds = ["835449167911924693", "835449167911924700"],
        JoinTimeBegin = DateTimeOffset.UtcNow.AddDays(-30).ToUnixTimeMilliseconds(),
        JoinTimeEnd = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        KycStatus = OkxAffiliateKycStatus.Verified,
        OrderBy = OkxAffiliateSortField.Volume,
        OrderDirection = OkxAffiliateSortDirection.Descending
    });

Console.WriteLine($"Pages: {invitees.Data?.TotalPages}");
foreach (var invitee in invitees.Data?.Items ?? [])
    Console.WriteLine($"{invitee.UserId}: {invitee.TotalVolume} USDT");

var inviteeDetail = await api.Affiliate.GetInviteeAsync(
    new OkxAffiliateInviteeDetailRequest
    {
        UserId = "835449167911924693",
        PeriodType = OkxAffiliatePeriodType.Last30Days
    });

Console.WriteLine($"30-day volume: {inviteeDetail.Data?.PeriodVolume} USDT");
```

## Method Catalog

- `GetPerformanceSummaryAsync`
- `GetInviteeAsync`
- `GetInviteesAsync`
- `GetAffiliateLinksAsync`
- `GetCoInviterLinksAsync`
- `GetSubAffiliatesAsync`
- `GetRebateInformationAsync`

## Tips

- Paginated responses return `OkxPaginatedResult<T>` so the root-level `totalPage` value is not lost. Use `TotalPages` and `Items` to continue paging.
- `periodType=custom` requires both inclusive `Begin` and `End` Unix-millisecond values. Invitee-list custom and join-time ranges are limited to 90 days, and both starts must be within the latest 180 days; the client rejects invalid ranges before sending.
- `UserIds` sends one or up to 100 exact external UIDs through the comma-separated `uid` parameter. Unknown UIDs are skipped by OKX; when none resolve, the endpoint returns an empty page rather than the unfiltered invitee list.
- `GetInviteeAsync` accepts a typed request when `PeriodType` and `PeriodVolume` are needed. `Custom` is not supported for detail queries, and `PeriodVolume` is omitted/null when no period is requested.
- The current Affiliate reporting endpoints have separate limits of 3 requests per second per User ID; the client registers each documented endpoint limit.
- `GetRebateInformationAsync` is a legacy endpoint that OKX marks for removal. Prefer `GetInviteeAsync`.


