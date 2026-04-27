# Funding Account

[Docs Home](../index.md) | [REST Reference](./index.md) | [Sub Account](./sub-account.md) | [Financial](./financial.md)

Official OKX docs: [Funding Account](https://www.okx.com/docs-v5/en/#funding-account)

## Overview

`api.Funding` covers balances, transfers, deposits, withdrawals, convert, fiat payment methods, monthly statements, and OKX buy/sell flows.

All `Funding` methods are private and require credentials.

## Example Calls

```csharp
var balances = await api.Funding.GetBalancesAsync();
var valuation = await api.Funding.GetAssetValuationAsync();
var depositAddress = await api.Funding.GetDepositAddressAsync("BTC");
var depositHistory = await api.Funding.GetDepositHistoryAsync();
var withdrawalHistory = await api.Funding.GetWithdrawalHistoryAsync();
```

Transfer and convert examples:

```csharp
await api.Funding.TransferAsync(
    OkxFundingTransferType.TransferWithinAccount,
    "USDT",
    100m,
    OkxAccount.Funding,
    OkxAccount.Trading);

var quote = await api.Funding.EstimateQuoteAsync(
    "BTC",
    "USDT",
    OkxTradeOrderSide.Sell,
    0.1m,
    "BTC");
```

## Method Catalog

### Balances and Valuation

- `GetCurrenciesAsync`
- `GetBalancesAsync`
- `GetNonTradableBalancesAsync`
- `GetAssetValuationAsync`

### Transfers and Bills

- `TransferAsync`
- `TransferStateAsync`
- `GetBillsAsync`
- `GetBillsHistoryAsync`

### Deposit and Withdrawal

- `GetDepositAddressAsync`
- `GetDepositHistoryAsync`
- `WithdrawAsync`
- `CancelWithdrawalAsync`
- `GetWithdrawalHistoryAsync`
- `GetDepositStatusAsync`
- `GetWithdrawalStatusAsync`
- `GetDepositWithdrawStatusAsync`
- `GetExchangesAsync`

### Statements

- `ApplyForMonthlyStatementAsync`
- `GetMonthlyStatementAsync`

### Convert

- `GetConvertCurrenciesAsync`
- `GetConvertCurrencyDetailsAsync`
- `GetConvertCurrencyPairAsync`
- `EstimateQuoteAsync`
- `PlaceConvertOrderAsync`
- `GetConvertHistoryAsync`

### Fiat Deposit and Withdrawal

- `GetDepositPaymentMethodsAsync`
- `GetWithdrawalPaymentMethodsAsync`
- `CreateWithdrawalOrderAsync`
- `CancelWithdrawalOrderAsync`
- `GetWithdrawalOrderHistoryAsync`
- `GetWithdrawalOrderDetailAsync`
- `GetDepositOrderHistoryAsync`
- `GetDepositOrderDetailAsync`

### Buy and Sell

- `GetBuySellCurrenciesAsync`
- `GetBuySellCurrencyPairAsync`
- `GetBuySellQuoteAsync`
- `PlaceBuySellTradeAsync`
- `GetBuySellTradeHistoryAsync`

## Request-Model Overloads

The following `Funding` methods have typed request-model overloads in addition to the shorter positional signatures:

- `TransferAsync(OkxFundingTransferRequest)`
- `TransferStateAsync(OkxFundingTransferStateRequest)`
- `GetBillsAsync(OkxFundingBillQueryRequest)`
- `GetBillsHistoryAsync(OkxFundingBillQueryRequest)`
- `GetDepositHistoryAsync(OkxFundingDepositHistoryRequest)`
- `WithdrawAsync(OkxFundingWithdrawalRequest)`
- `GetWithdrawalHistoryAsync(OkxFundingWithdrawalHistoryRequest)`
- `GetConvertCurrencyPairAsync(OkxFundingConvertCurrencyPairRequest)`
- `EstimateQuoteAsync(OkxFundingConvertEstimateQuoteRequest)`
- `PlaceConvertOrderAsync(OkxFundingConvertOrderRequest)`
- `GetConvertHistoryAsync(OkxFundingConvertHistoryRequest)`
- `CreateWithdrawalOrderAsync(OkxFundingFiatWithdrawalOrderRequest)`
- `GetWithdrawalOrderHistoryAsync(OkxFundingFiatOrderHistoryRequest)`
- `GetDepositOrderHistoryAsync(OkxFundingFiatOrderHistoryRequest)`
- `GetBuySellQuoteAsync(OkxFundingBuySellQuoteRequest)`
- `PlaceBuySellTradeAsync(OkxFundingBuySellTradeRequest)`
- `GetBuySellTradeHistoryAsync(OkxFundingBuySellHistoryRequest)`

## Tips

- Funding operations move real assets. Treat examples as live-account actions unless you explicitly use demo endpoints that support the route.
- The convert and fiat flows benefit the most from typed request-model overloads.


