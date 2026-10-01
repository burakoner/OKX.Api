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
| 2026-08-11 | RPI spacing/visibility; complete endpoint comparison also found Amend wire/timestamp gaps and missing WS deadlines | Verified 2026-10-01 for behavior/wire changes; full trading parity awaits the limiter decision below |
| Limiter scope decision | REST/WS shared instrument/family budgets, weighted batches, single-order batch routing, and account-specific lower limits | Awaiting decision; existing guards unchanged |
| 2026-08-18 | RPI minimum notional and error 54051 across placement/amendment; use current September 15 thresholds immediately | Pending |
| 2026-08-20 | Post-only / MMP post-only / RPI order-state transitions | Pending |
| 2026-08-26 | Public delta-hedge currencies endpoint | Pending |
| Review checkpoint | Retrospective code/docs review and forward scope/order revision after 4-5 development turns; move earlier if the limiter step is added | Pending |
| 2026-09-15 | Reverify current RPI thresholds; change only remaining differences | Pending |
| 2026-09-30 | Instrument deltas, Crypto-USD to Crypto-USDC, tradeQuoteCcy defaults, and account activate-feature endpoint | Pending |
| Final review | Documentation-supported cross-surface regression review | Pending |

Discovery source: [OKX API changelog](https://www.okx.com/docs-v5/log_en/). A newly discovered production compatibility risk can move ahead of this sequence; record the evidence and revised scope here.

Prerequisite verification: 21 focused address/history tests and all 456 offline tests passed; both netstandard targets built with zero warnings/errors. The full suite exposed an old Affiliate test whose fixed April inputs had expired against the rolling 180-day validation; only its test inputs were corrected. No live trading or WebSocket connection test was performed.

August 11 scope revision: the changelog is behavioral only, but current Place/Amend tables exposed additional request/response omissions, so string amendment values, creation timestamps, and optional WS effective deadlines belong in this step. The full comparison also found that existing throttling does not coordinate the official REST/WS shared, per-instrument/family, per-order batch budgets or account-specific lower limits. Proposed next scope: a small separate limiter-design/implementation step before claiming complete trading parity. Do not silently raise existing limits or invent account state. The scheduled retrospective review remains required.

August 11 verification: 78 focused trading/order-book cases and all 473 offline tests passed; both netstandard targets built with zero warnings/errors. Local signed requests verified amendment string values, opt-in command-root deadlines, acknowledgement creation times, and request immutability. Synthetic organic-only fixtures verify row parsing and sequence metadata, not matching-engine visibility or production enforcement. No live orders or private WebSocket connections were used.

Documentation uncertainty: current WS Place/Amend tables omit `attachAlgoOrds`, unlike REST. Existing serialization is preserved; there is no explicit decommissioning notice and serialization is not proof of WS support. Keep this caveat visible until the official contract clarifies it; do not resolve it by sending real orders.

## Operational Watch Items

- **October 31:** [8443 shutdown](https://www.okx.com/en-us/help/okx-websocket-port-8443-discontinuation-announcement). Built-in URLs will use 443; applications overriding URLs must migrate separately.
- **October 31:** Legacy ELP naming transition ends according to the July 28 changelog. Keep current RPI names primary and reassess compatibility aliases against current docs; do not remove public aliases blindly.
- SBE trading/private-channel support is announced for later rollout. Track separately from this JSON-wrapper catch-up; do not expand implementation scope without agreement.
- An old SignalClone upcoming entry remains unconfirmed by a matching current endpoint section. Treat it as unresolved discovery, not an implementable contract.
