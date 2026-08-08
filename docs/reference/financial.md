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
- `api.Financial.Okusd`

Most methods are private. A few `SimpleEarn` borrow summary/history reads are public.

## Example Calls

```csharp
var onChainOffers = await api.Financial.OnChainEarn.GetOffersAsync();
var ethProduct = await api.Financial.EthStaking.GetProductInfoAsync();
var solProduct = await api.Financial.SolStaking.GetProductInfoAsync();
var simpleEarnBalances = await api.Financial.SimpleEarn.GetBalancesAsync();
var flexibleLoanInfo = await api.Financial.FlexibleLoan.GetLoanInfoAsync();
var dualInvestmentPairs = await api.Financial.DualInvestment.GetCurrencyPairsAsync();
var okusdLimits = await api.Financial.Okusd.GetLimitsAsync();
var okusdSubscription = await api.Financial.Okusd.SubscribeAsync(1000m, "subscription-20260703-1");
var okusdRedemption = await api.Financial.Okusd.RedeemAsync(
    1000m,
    OkxFinancialOkusdRedemptionType.Fast,
    "redemption-20260703-1");
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

### OKUSD

- `GetLimitsAsync`
- `SubscribeAsync`
- `RedeemAsync`

The limits endpoint returns the subscription, fast-redemption, and standard-redemption quotas shared by the master account and its subaccounts. `SubscribeAsync` converts USDT to OKUSD at 1:1 without a subscription fee. `RedeemAsync` supports fast real-time settlement and standard D+5/D+6 calendar-day settlement; obtain the current fee rates from `GetLimitsAsync` before redeeming.

Both order methods require an amount of at least `1` with no more than 8 decimal places. Their `clOrdId` values accept 1–32 letters, digits, hyphens, or underscores and are idempotency keys per UID: reusing one returns the original OKX order instead of executing another transfer.

OKX currently documents error codes `51763`–`51774` for VIP eligibility, insufficient balance, user/platform quotas, maintenance, liquidity, and regional availability. These are returned through the library's standard `ServerError` path with the original numeric code and message.

## Tips

- Product families differ significantly; keep each one in its own service layer instead of treating `Financial` as one uniform API.
- Public borrow summary/history methods inside `SimpleEarn` are safe to use without credentials.


