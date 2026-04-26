# OKX.Api.Tests

This test project uses two fixture layers:

- `Fixtures/Manual`: hand-authored JSON that targets edge cases and changelog-driven regressions
- `Fixtures/Live/Production`: current OKX production public response snapshots
- `Fixtures/Live/Demo`: current OKX demo-mode public response snapshots captured with `x-simulated-trading: 1`

The capture script always refreshes the production snapshots. When `OKX_DEMO_TRADING=true` is set in `.env`, it also refreshes the demo snapshots so we can validate behavior differences such as large `instIdCode` values or endpoints that are unavailable in demo trading.

Some public-data endpoints, such as Event Contracts, are authenticated by OKX even though they live under `/public`. If `OKX_API_KEY`, `OKX_API_SECRET`, and `OKX_API_PASSPHRASE` are present in `.env`, the capture script also refreshes those authenticated public snapshots.

The committed live fixtures are public-only on purpose. Private account payloads may contain sensitive account data, so private coverage should stay manual or be sanitized before it is ever committed.

## Refreshing live fixtures

Run:

```powershell
.\scripts\Capture-LiveFixtures.ps1
```

Optional overrides can be placed in the local `.env` file:

- `OKX_CAPTURE_PUBLIC_OPTION_ULY`
- `OKX_PUBLIC_EVENTS_SERIES_ID`
- `OKX_CAPTURE_PUBLIC_EVENTS_SERIES_ID`
- `OKX_CAPTURE_PUBLIC_BORROW_CURRENCY`
- `OKX_CAPTURE_PUBLIC_FUNDING_HISTORY_XPERP_INST_ID`

Integration tests are still opt-in and continue to use `.env`.
