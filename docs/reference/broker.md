# Broker

[Docs Home](../index.md) | [REST Reference](./index.md) | [Affiliate](./affiliate.md)

Official OKX docs: [OKX Broker API](https://www.okx.com/docs-v5/broker_en/)

## Overview

`api.Broker` is a container for broker-specific clients:

- `api.Broker.FD` for fully disclosed broker endpoints
- `api.Broker.DMA` for DMA broker endpoints

## Current Implementation Status

At the moment, the broker section primarily exposes the client structure and credential wiring. The concrete REST methods for FD and DMA broker operations are still marked as TODO in the codebase.

That means:

- the section is visible and documented so users understand the intended shape
- it should not yet be treated as feature-complete

## Example Access

```csharp
var broker = api.Broker;
var fdClient = api.Broker.FD;
var dmaClient = api.Broker.DMA;
```

## What To Expect

The intended long-term coverage includes:

- broker rebate endpoints
- DMA sub-account administration
- DMA deposit-address workflows
- DMA rebate and credit endpoints

Until those methods are implemented, prefer the official OKX broker documentation directly for exact endpoint coverage.


