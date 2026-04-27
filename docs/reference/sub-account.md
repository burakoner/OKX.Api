# Sub Account

[Docs Home](../index.md) | [REST Reference](./index.md) | [Funding](./funding.md) | [Account](./account.md)

Official OKX docs: [Sub Account](https://www.okx.com/docs-v5/en/#sub-account)

## Overview

`api.SubAccount` handles sub-account creation, key management, balances, bills, transfer permissions, and sub-account transfers.

All `SubAccount` methods are private and require credentials.

## Example Calls

```csharp
var subs = await api.SubAccount.GetSubAccountsAsync();
var tradingBalance = await api.SubAccount.GetSubAccountTradingBalancesAsync("sub-account-name");
var fundingBalances = await api.SubAccount.GetSubAccountFundingBalancesAsync("sub-account-name");
var maximumWithdrawals = await api.SubAccount.GetSubAccountMaximumWithdrawalsAsync("sub-account-name");
```

Administrative examples:

```csharp
var created = await api.SubAccount.CreateSubAccountAsync("new-sub-account", 1);
var apiKey = await api.SubAccount.CreateSubAccountApiKeyAsync(
    "new-sub-account",
    "automation",
    "strong-passphrase",
    readPermission: true,
    tradePermission: true);
```

## Method Catalog

Overload note: list, create, API-key reset, bills, managed bills, and transfer methods also expose typed request-model overloads.

### Account and API Key Management

- `GetSubAccountsAsync`
- `CreateSubAccountAsync`
- `CreateSubAccountApiKeyAsync`
- `GetSubAccountApiKeysAsync`
- `ResetSubAccountApiKeyAsync`
- `DeleteSubAccountApiKeyAsync`

### Balances and Bills

- `GetSubAccountTradingBalancesAsync`
- `GetSubAccountFundingBalancesAsync`
- `GetSubAccountMaximumWithdrawalsAsync`
- `GetSubAccountBillsAsync`
- `GetSubAccountManagedBillsAsync`

### Transfers and Permissions

- `TransferBetweenSubAccountsAsync`
- `SetPermissionOfTransferOutAsync`
- `GetCustodySubAccountsAsync`

## Tips

- Sub-account administration is sensitive; make sure your API key actually has the required account-level permissions before assuming a wrapper issue.
- Some sub-account list and bill endpoints are easiest to use through request-model overloads when you need multiple filters.


