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

## Request-Model Overloads

The following `SubAccount` methods have typed request-model overloads in addition to the shorter positional signatures:

- `GetSubAccountsAsync(OkxSubAccountListRequest)`
- `CreateSubAccountAsync(OkxSubAccountCreateRequest)`
- `CreateSubAccountApiKeyAsync(OkxSubAccountApiKeyCreateRequest)`
- `ResetSubAccountApiKeyAsync(OkxSubAccountApiKeyResetRequest)`
- `GetSubAccountBillsAsync(OkxSubAccountBillQueryRequest)`
- `GetSubAccountManagedBillsAsync(OkxSubAccountManagedBillQueryRequest)`
- `TransferBetweenSubAccountsAsync(OkxSubAccountTransferRequest)`

## Request-Model Examples

```csharp
var subs = await api.SubAccount.GetSubAccountsAsync(new OkxSubAccountListRequest
{
    Enable = true,
    Limit = 50
});

var created = await api.SubAccount.CreateSubAccountAsync(new OkxSubAccountCreateRequest
{
    SubAccountName = "new-sub-account",
    Type = OkxSubAccountType.StandardSubAccount
});

var apiKey = await api.SubAccount.CreateSubAccountApiKeyAsync(new OkxSubAccountApiKeyCreateRequest
{
    SubAccountName = "new-sub-account",
    Label = "automation",
    Passphrase = "strong-passphrase",
    ReadPermission = true,
    TradePermission = true
});

await api.SubAccount.ResetSubAccountApiKeyAsync(new OkxSubAccountApiKeyResetRequest
{
    SubAccountName = "new-sub-account",
    ApiKey = "existing-api-key",
    TradePermission = true
});

var subBills = await api.SubAccount.GetSubAccountBillsAsync(new OkxSubAccountBillQueryRequest
{
    SubAccountName = "new-sub-account",
    Currency = "USDT",
    Limit = 100
});

var managedBills = await api.SubAccount.GetSubAccountManagedBillsAsync(new OkxSubAccountManagedBillQueryRequest
{
    Currency = "USDT",
    Limit = 100
});

await api.SubAccount.TransferBetweenSubAccountsAsync(new OkxSubAccountTransferRequest
{
    Currency = "USDT",
    Amount = 25m,
    FromAccount = OkxAccount.Funding,
    ToAccount = OkxAccount.Trading,
    FromSubAccountName = "source-sub",
    ToSubAccountName = "target-sub"
});
```

## Tips

- Sub-account administration is sensitive; make sure your API key actually has the required account-level permissions before assuming a wrapper issue.
- Some sub-account list and bill endpoints are easiest to use through request-model overloads when you need multiple filters.


