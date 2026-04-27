# Financial Products

[Docs Home](../index.md) | [REST Reference](./index.md) | [Funding](./funding.md) | [Trade](./trade.md)

Official OKX docs: [Financial Product](https://www.okx.com/docs-v5/en/#financial-product)

## Overview

`api.Financial` is a container for multiple product families:

- `api.Financial.OnChainEarn`
- `api.Financial.EthStaking`
- `api.Financial.SolStaking`
- `api.Financial.SimpleEarn`
- `api.Financial.FlexibleLoan`
- `api.Financial.DualInvestment`

Most methods are private. A few `SimpleEarn` borrow summary/history reads are public.

## Example Calls

```csharp
var onChainOffers = await api.Financial.OnChainEarn.GetOffersAsync();
var ethProduct = await api.Financial.EthStaking.GetProductInfoAsync();
var solProduct = await api.Financial.SolStaking.GetProductInfoAsync();
var simpleEarnBalances = await api.Financial.SimpleEarn.GetBalancesAsync();
var flexibleLoanInfo = await api.Financial.FlexibleLoan.GetLoanInfoAsync();
var dualInvestmentPairs = await api.Financial.DualInvestment.GetCurrencyPairsAsync();
```

## Method Catalog

### On-Chain Earn

- `GetOffersAsync`
- `PurchaseAsync`
- `RedeemAsync`
- `CancelAsync`
- `GetOpenOrdersAsync`
- `GetHistoryAsync`

### ETH Staking

- `GetProductInfoAsync`
- `PurchaseAsync`
- `RedeemAsync`
- `CancelRedeemAsync`
- `GetBalancesAsync`
- `GetHistoryAsync`
- `GetApyHistoryAsync`

### SOL Staking

- `GetProductInfoAsync`
- `PurchaseAsync`
- `RedeemAsync`
- `GetBalancesAsync`
- `GetHistoryAsync`
- `GetApyHistoryAsync`

### Simple Earn

- `GetBalancesAsync`
- `PurchaseAsync`
- `RedeemAsync`
- `SetLendingRateAsync`
- `GetLendingHistoryAsync`
- `GetPublicBorrowSummaryAsync`
- `GetPublicBorrowHistoryAsync`

### Flexible Loan

- `GetBorrowableCurrenciesAsync`
- `GetCollateralAssetsAsync`
- `GetMaximumLoanAmountAsync`
- `GetMaximumCollateralRedeemAmountAsync`
- `AdjustCollateralAsync`
- `GetLoanInfoAsync`
- `GetLoanHistoryAsync`
- `GetAccruedInterestAsync`

### Dual Investment

- `GetCurrencyPairsAsync`
- `GetProductsAsync`
- `RequestQuoteAsync`
- `TradeAsync`
- `RequestRedeemQuoteAsync`
- `RedeemAsync`
- `GetOrderStatusAsync`
- `GetOrderHistoryAsync`

## Tips

- Product families differ significantly; keep each one in its own service layer instead of treating `Financial` as one uniform API.
- Public borrow summary/history methods inside `SimpleEarn` are safe to use without credentials.


