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
        KycStatus = OkxAffiliateKycStatus.Verified,
        OrderBy = OkxAffiliateSortField.Volume,
        OrderDirection = OkxAffiliateSortDirection.Descending
    });

Console.WriteLine($"Pages: {invitees.Data?.TotalPages}");
foreach (var invitee in invitees.Data?.Items ?? [])
    Console.WriteLine($"{invitee.UserId}: {invitee.TotalVolume} USDT");
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
- `periodType=custom` requires both inclusive `Begin` and `End` Unix-millisecond values. Invitee list custom and join-time ranges are limited to 90 days; OKX also requires their start to be within the latest 180 days.
- `GetInviteeAsync` accepts a typed request when `PeriodType` and `PeriodVolume` are needed. The current Affiliate reporting endpoints are limited to 3 requests per second per user.
- `GetRebateInformationAsync` is a legacy endpoint that OKX marks for removal. Prefer `GetInviteeAsync`.


