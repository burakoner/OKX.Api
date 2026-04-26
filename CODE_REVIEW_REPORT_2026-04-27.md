# OKX.Api Code Review Report

Date: 2026-04-27

Status update:
- Finding 1 was fixed afterward in `OkxPublicSocketClient` by forwarding the single-symbol overload to the collection overload.
- Findings 2, 3, and 4 were fixed afterward in `OkxBaseSocketClient`.
- Regression coverage was added afterward for unsubscribe ack handling and subscription/message routing with `ccy`, `algoId`, and `extraParams`.

Scope:
- General review of the `OKX.Api` solution
- Focus on runtime correctness, WebSocket behavior, and finance-safety risks
- Verified current baseline with `dotnet test "OKX Api Client.sln" -c Release --no-restore` -> `104 passed`

## Executive Summary

The project is in a much stronger state than a typical wrapper library: the solution builds cleanly, the test suite is substantial, and the recent fixture-based/live-response strategy is already paying off.

That said, I found four issues worth addressing before treating the current WebSocket layer as fully reliable in production. Two are high severity because they can break runtime behavior directly, and two are medium severity because they can silently mis-correlate subscriptions under realistic multi-subscription usage.

## Findings

### 1. High: `SubscribeToCallAuctionsAsync(string)` recurses into itself and will stack overflow

Files:
- `OKX.Api/Public/Clients/OkxPublicSocketClient.cs:279`
- `OKX.Api/Public/Clients/OkxPublicSocketClient.cs:280`

What happens:
- The single-symbol overload calls `SubscribeToCallAuctionsAsync(onData, instrumentId, ct)`.
- That call resolves back to the same overload instead of the `IEnumerable<string>` overload.
- Any direct use of this method will recurse until stack overflow.

Why this matters:
- This is a hard runtime failure on a public API surface.
- It is especially risky because the method signature looks completely normal to downstream users.

Suggested fix:
- Forward to the collection overload with `[instrumentId]`, matching the pattern used elsewhere in the file.

Missing coverage:
- There is currently no test that exercises `SubscribeToCallAuctionsAsync(string, ...)`.

### 2. High: WebSocket unsubscribe path always returns `false`, even after a successful unsubscribe ack

Files:
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:298`
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:304`
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:321`

What happens:
- `UnsubscribeAsync` waits for an `"unsubscribe"` response.
- Even if the response matches and the lambda returns `true`, the outer method still ends with `return false;`.

Why this matters:
- The base WebSocket client may treat unsubscription as failed even when the server accepted it.
- That can leave stale subscriptions registered locally, cause duplicate handlers over time, or make caller-side cleanup unreliable.

Why I consider this production-relevant:
- This is in the shared base class, so the behavior affects every socket module built on top of it.

Missing coverage:
- I did not find a regression test for unsubscribe success behavior.

### 3. Medium: WebSocket message correlation ignores `ccy`, `algoId`, and `extraParams`

Files:
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:274`
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:279`

Examples of request builders that rely on ignored fields:
- `OKX.Api/Account/Clients/OkxAccountSocketClient.cs:33`
- `OKX.Api/Account/Clients/OkxAccountSocketClient.cs:37`
- `OKX.Api/Funding/Clients/OKXFundingSocketClient.cs:50`
- `OKX.Api/Funding/Clients/OKXFundingSocketClient.cs:103`
- `OKX.Api/RecurringBuy/Clients/OkxRecurringBuySocketClient.cs:28`
- `OKX.Api/RecurringBuy/Clients/OkxRecurringBuySocketClient.cs:33`

What happens:
- Incoming subscription messages are matched only by `channel`, `instId`, `instType`, and `instFamily`.
- Subscriptions that differ by `ccy`, `algoId`, or `extraParams` are treated as equivalent by the router.

Why this matters:
- Two subscriptions on the same channel but with different currencies or intervals can be cross-routed.
- The most obvious risk is on:
  - `account` channel with `ccy` and `interval`
  - `deposit-info` / `withdrawal-info` with `ccy`
  - `algo-recurring-buy` with `algoId`

Likely user-visible symptom:
- A caller can subscribe to multiple filtered streams and receive updates through the wrong handler, or receive more data than expected.

Missing coverage:
- I did not find concurrency or same-channel multi-filter tests covering these cases.

### 4. Medium: Subscription success acks are correlated by `channel` only

Files:
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:223`
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:229`
- `OKX.Api/Base/Clients/OkxBaseSocketClient.cs:233`

What happens:
- When a `"subscribe"` ack is received, the handler checks only whether any outgoing arg has the same channel name.
- It does not verify `instId`, `instType`, `instFamily`, `ccy`, `algoId`, or `extraParams`.

Why this matters:
- Concurrent subscriptions to the same channel can be acknowledged by the wrong ack.
- This can hide partial failures, especially when callers subscribe separately instead of batching all args into one request.

Why this is different from Finding 3:
- Finding 3 is about routing live data messages.
- This one is about confirming that the intended subscription was actually accepted by the server.

## Positive Notes

- The test setup is a strong foundation for an open-source financial wrapper.
- Manual fixtures plus live snapshots are the right strategy here.
- The recent work on deserialization hardening, `instIdCode`, `subCode`, and event contracts clearly improved resilience.
- Live integration coverage with `.env` is already catching real-world payload drift that documentation-only work would miss.

## Testing Gaps

The current suite is good, but these specific areas are still under-covered:

- No direct regression test for `SubscribeToCallAuctionsAsync(string, ...)`
- No unsubscribe success/failure lifecycle test
- No multi-subscription correlation test for same-channel subscriptions with different `ccy` / `interval` / `algoId`
- No ack-correlation test for concurrent same-channel subscribe operations

## Recommended Next Steps

1. Fix the call-auction overload recursion and add a small unit test for the single-symbol overload.
2. Fix `UnsubscribeAsync` so it returns actual success state from the server ack path.
3. Expand `OkxSocketRequestArgument` matching to include all routing-relevant fields:
   - `ccy`
   - `algoId`
   - `extraParams`
4. Add focused WebSocket correlation tests before further socket-surface expansion.
