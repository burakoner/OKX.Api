# Maintenance Execution Contract

[Docs Home](./index.md) | [Changelog](./changelog/2026.md)

Baseline: OKX changelog synchronized through **2026-08-06**; package version **5.6.808**. Catch-up assessment: **2026-10-01**. This is a living execution contract, not a frozen instruction list.

## Scope and Verification

- Changelog dates determine discovery and sequence, not a historical target. Compare every touched endpoint/channel's complete contract with its current official documentation, including request, response, behavior, and rate limits.
- If a later date revisits an already synchronized contract, verify it without adding duplicate changes. Record conflicts between announcements, tables, and examples explicitly.
- Keep changes minimal and preserve user edits. Each development turn ends with targeted verification and an explanatory local commit; no push.
- Use deterministic fixtures/local servers for trading contracts. Do not place live orders, automatically activate account features, or treat local tests as proof of production behavior.
- After 4-5 development turns, review preceding changes against documentation and revise upcoming scope/order before continuing.

## Current Sequence

| Step | Scope | Status |
| --- | --- | --- |
| Prerequisites | WebSocket 8443 to 443; historical UTC instant vs module calendar-date review | Completed 2026-10-01 |
| 2026-08-11 | RPI spacing/visibility; complete endpoint comparison also found Amend wire/timestamp gaps and missing WS deadlines | Behavior/wire changes verified 2026-10-01; configured limiter follow-up and documentation caveats recorded below |
| Limiter follow-up | Explicit opt-in shared Place/Amend instrument/family/account budgets, weighted batches, one-order batch routing, and current account-query guard | Completed 2026-10-01 within the documented configured-guard boundaries |
| 2026-08-18 | RPI minimum notional and error 54051 across placement/amendment; use current September 15 thresholds immediately | Pending |
| 2026-08-20 | Post-only / MMP post-only / RPI order-state transitions | Pending |
| Review checkpoint | Retrospective code/docs review and forward scope/order revision after prerequisites, August 11, limiter, August 18, and August 20 (five development turns) | Moved ahead of August 26 after the limiter step was approved |
| 2026-08-26 | Public delta-hedge currencies endpoint | Pending after review |
| 2026-09-15 | Reverify current RPI thresholds; change only remaining differences | Pending |
| 2026-09-30 | Instrument deltas, Crypto-USD to Crypto-USDC, tradeQuoteCcy defaults, and account activate-feature endpoint | Pending |
| Final review | Documentation-supported cross-surface regression review | Pending |

Discovery source: [OKX API changelog](https://www.okx.com/docs-v5/log_en/). A newly discovered production compatibility risk can move ahead of this sequence; record the evidence and revised scope here.

Prerequisite verification: 21 focused address/history tests and all 456 offline tests passed; both netstandard targets built with zero warnings/errors. The full suite exposed an old Affiliate test whose fixed April inputs had expired against the rolling 180-day validation; only its test inputs were corrected. No live trading or WebSocket connection test was performed.

August 11 scope revision: the changelog is behavioral only, but current Place/Amend tables exposed additional request/response omissions, so string amendment values, creation timestamps, and optional WS effective deadlines belong in this step. The full comparison also found that existing throttling does not coordinate the official REST/WS shared, per-instrument/family, per-order batch budgets or account-specific lower limits. Proposed next scope: a small separate limiter-design/implementation step before claiming complete trading parity. Do not silently raise existing limits or invent account state. The scheduled retrospective review remains required.

August 11 verification: 78 focused trading/order-book cases and all 473 offline tests passed; both netstandard targets built with zero warnings/errors. Local signed requests verified amendment string values, opt-in command-root deadlines, acknowledgement creation times, and request immutability. Synthetic organic-only fixtures verify row parsing and sequence metadata, not matching-engine visibility or production enforcement. No live orders or private WebSocket connections were used.

Documentation uncertainty: current WS Place/Amend tables omit `attachAlgoOrds`, unlike REST. Existing serialization is preserved; there is no explicit decommissioning notice and serialization is not proof of WS support. Keep this caveat visible until the official contract clarifies it; do not resolve it by sending real orders.

Limiter follow-up (approved after August 11): added a small explicit opt-in, fail-fast guard rather than a credential-keyed global registry or autonomous account polling. A single instance represents one User ID/environment; the caller must share it with every relevant REST/WS client, register current metadata and known lead status, and apply current account caps explicitly. Null preserves legacy throttling, which still does not reproduce the shared contract. Existing transport guards remain active; no throughput increase is claimed.

The guard uses atomic weighted reservations across instrument/Options-family and account budgets; Place/Amend and single/batch budgets remain independent except the documented one-order-batch routing. Pending commands remain counted through transport queues; completed/failed/uncertain commands remain counted for two seconds after completion. SPOT/MARGIN and known MMP placement exemptions are represented; unknown original-type MMP amendments are counted conservatively. Cancellation, other processes, server-side three-in-progress amendment enforcement, and account/lead-state discovery are intentionally outside this guard. These boundaries are explicit, not a claim of exact server behavior or universal default parity.

The complete [Account rate limit endpoint](https://www.okx.com/docs-v5/en/#order-book-trading-trade-get-account-rate-limit) was also reviewed: response fields already match; the missing 1-request-per-second guard was added. Querying it does not override a lower application cap or activate `nextAccRateLimit` early. Verification and configuration examples are in the [trade reference](./reference/trade.md#trading-rate-limit-scope).

Limiter verification: all 501 offline tests passed (28 new cases), and both netstandard targets built with zero warnings/errors. Tests use a controllable monotonic clock, concurrent weighted reservations, local signed REST requests, and recorded WS query calls; no production/demonstration orders or private network connections were sent. The expanded suite exposed a test-server port-probe/prefix-registration race; fixture startup is now serialized, failed listeners are closed, and concurrent live-prefix creation has its own regression. The final diff review also retained legacy unconfigured cancellation behavior and restricted MMP placement exemption to registered Options metadata. Package version stays 5.6.811 with an unreleased follow-up entry until the next dated release.

## Operational Watch Items

- **October 31:** [8443 shutdown](https://www.okx.com/en-us/help/okx-websocket-port-8443-discontinuation-announcement). Built-in URLs will use 443; applications overriding URLs must migrate separately.
- **October 31:** Legacy ELP naming transition ends according to the July 28 changelog. Keep current RPI names primary and reassess compatibility aliases against current docs; do not remove public aliases blindly.
- SBE trading/private-channel support is announced for later rollout. Track separately from this JSON-wrapper catch-up; do not expand implementation scope without agreement.
- An old SignalClone upcoming entry remains unconfirmed by a matching current endpoint section. Treat it as unresolved discovery, not an implementable contract.
