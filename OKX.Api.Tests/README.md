# OKX.Api.Tests

This test project uses two fixture layers:

- `Fixtures/Manual`: hand-authored JSON that targets edge cases and changelog-driven regressions
- `Fixtures/Live/Production`: current OKX production public response snapshots
- `Fixtures/Live/Demo`: current OKX demo-mode public response snapshots captured with `x-simulated-trading: 1`

The capture script always refreshes the production snapshots. When `OKX_DEMO_TRADING=true` is set in `.env`, it also refreshes the demo snapshots so we can validate behavior differences such as large `instIdCode` values or endpoints that are unavailable in demo trading.

The committed live fixtures are public-only on purpose. Private account payloads may contain sensitive account data, so private coverage should stay manual or be sanitized before it is ever committed.

## Refreshing live fixtures

Run:

```powershell
.\scripts\Capture-LiveFixtures.ps1
```

Optional overrides can be placed in the local `.env` file:

- `OKX_CAPTURE_PUBLIC_OPTION_ULY`
- `OKX_CAPTURE_PUBLIC_EVENTS_SERIES_ID`
- `OKX_CAPTURE_PUBLIC_BORROW_CURRENCY`

Integration tests are still opt-in and continue to use `.env`.
