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

## Request-Model Examples

```csharp
await api.Funding.TransferAsync(new OkxFundingTransferRequest
{
    Type = OkxFundingTransferType.TransferWithinAccount,
    Currency = "USDT",
    Amount = 100m,
    FromAccount = OkxAccount.Funding,
    ToAccount = OkxAccount.Trading
});

var transferState = await api.Funding.TransferStateAsync(new OkxFundingTransferStateRequest
{
    ClientOrderId = "transfer-001"
});

var bills = await api.Funding.GetBillsAsync(new OkxFundingBillQueryRequest
{
    Currency = "USDT",
    Limit = 100
});

var billHistory = await api.Funding.GetBillsHistoryAsync(new OkxFundingBillQueryRequest
{
    Currency = "BTC",
    Limit = 100
});

var depositHistory = await api.Funding.GetDepositHistoryAsync(new OkxFundingDepositHistoryRequest
{
    Currency = "BTC",
    Limit = 50
});

await api.Funding.WithdrawAsync(new OkxFundingWithdrawalRequest
{
    Currency = "USDT",
    Amount = 50m,
    Destination = OkxFundingWithdrawalDestination.DigitalCurrencyAddress,
    ToAddress = "destination-address",
    ToAddressType = OkxFundingWithdrawalAddressType.AddressInfo,
    Chain = "USDT-TRC20"
});

var withdrawalHistory = await api.Funding.GetWithdrawalHistoryAsync(new OkxFundingWithdrawalHistoryRequest
{
    Currency = "USDT",
    Limit = 50
});

var convertPairs = await api.Funding.GetConvertCurrencyPairAsync(new OkxFundingConvertCurrencyPairRequest
{
    FromCurrency = "BTC",
    ToCurrency = "USDT"
});

var convertQuote = await api.Funding.EstimateQuoteAsync(new OkxFundingConvertEstimateQuoteRequest
{
    BaseCurrency = "BTC",
    QuoteCurrency = "USDT",
    Side = OkxTradeOrderSide.Sell,
    RfqAmount = 0.1m,
    RfqCurrency = "BTC"
});

await api.Funding.PlaceConvertOrderAsync(new OkxFundingConvertOrderRequest
{
    QuoteId = "quote-id",
    BaseCurrency = "BTC",
    QuoteCurrency = "USDT",
    Side = OkxTradeOrderSide.Sell,
    Amount = 0.1m,
    AmountCurrency = "BTC"
});

var convertHistory = await api.Funding.GetConvertHistoryAsync(new OkxFundingConvertHistoryRequest
{
    Limit = 50
});

await api.Funding.CreateWithdrawalOrderAsync(new OkxFundingFiatWithdrawalOrderRequest
{
    PaymentAccountId = "payment-account-id",
    Currency = "EUR",
    Amount = 100m,
    PaymentMethod = "bank_transfer"
});

var fiatWithdrawals = await api.Funding.GetWithdrawalOrderHistoryAsync(new OkxFundingFiatOrderHistoryRequest
{
    Currency = "EUR",
    Limit = 20
});

var fiatDeposits = await api.Funding.GetDepositOrderHistoryAsync(new OkxFundingFiatOrderHistoryRequest
{
    Currency = "EUR",
    Limit = 20
});

var buySellQuote = await api.Funding.GetBuySellQuoteAsync(new OkxFundingBuySellQuoteRequest
{
    Side = OkxTradeOrderSide.Buy,
    FromCurrency = "EUR",
    ToCurrency = "BTC",
    RfqAmount = 100m,
    RfqCurrency = "EUR"
});

await api.Funding.PlaceBuySellTradeAsync(new OkxFundingBuySellTradeRequest
{
    QuoteId = "quote-id",
    Side = OkxTradeOrderSide.Buy,
    FromCurrency = "EUR",
    ToCurrency = "BTC",
    RfqAmount = 100m,
    RfqCurrency = "EUR",
    PaymentMethod = "bank_transfer"
});

var buySellHistory = await api.Funding.GetBuySellTradeHistoryAsync(new OkxFundingBuySellHistoryRequest
{
    Limit = 20
});
```

## Tips

- Funding operations move real assets. Treat examples as live-account actions unless you explicitly use demo endpoints that support the route.
- The convert and fiat flows benefit the most from typed request-model overloads.


