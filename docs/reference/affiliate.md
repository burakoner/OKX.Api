# Affiliate

[Docs Home](../index.md) | [REST Reference](./index.md) | [Broker](./broker.md)

Official OKX docs: [Affiliate](https://www.okx.com/docs-v5/en/#affiliate)

## Overview

`api.Affiliate` is a small private section focused on affiliate data.

It requires credentials.

## Example Calls

```csharp
var invitee = await api.Affiliate.GetInviteeAsync(1000000);
var rebateInfo = await api.Affiliate.GetRebateInformationAsync("affiliate-api-key");
```

## Method Catalog

- `GetInviteeAsync`
- `GetRebateInformationAsync`

## Tips

- `GetRebateInformationAsync` expects the affiliate API key that OKX associates with the rebate query.
- This client is intentionally small; if OKX expands affiliate coverage later, it can grow without affecting unrelated sections.


