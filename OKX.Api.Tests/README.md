# OKX.Api.Tests

This test project uses two fixture layers:

- `Fixtures/Manual`: hand-authored JSON that targets edge cases and changelog-driven regressions
- `Fixtures/Live`: current OKX public response snapshots captured from live endpoints

The committed live fixtures are public-only on purpose. Private account payloads may contain sensitive account data, so private coverage should stay manual or be sanitized before it is ever committed.

## Refreshing live fixtures

Run:

```powershell
.\scripts\Capture-LiveFixtures.ps1
```

Optional overrides can be placed in the local `.env` file:

- `OKX_CAPTURE_PUBLIC_SPOT_INST_ID`
- `OKX_CAPTURE_PUBLIC_COMMODITY_INST_TYPE`
- `OKX_CAPTURE_PUBLIC_COMMODITY_INST_ID`
- `OKX_CAPTURE_PUBLIC_BORROW_CURRENCY`

Integration tests are still opt-in and continue to use `.env`.
