param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Load-DotEnv {
    param(
        [string]$Path
    )

    if (-not (Test-Path -LiteralPath $Path)) {
        return
    }

    foreach ($line in Get-Content -LiteralPath $Path) {
        $trimmed = $line.Trim()
        if ([string]::IsNullOrWhiteSpace($trimmed) -or $trimmed.StartsWith("#")) {
            continue
        }

        $separatorIndex = $trimmed.IndexOf("=")
        if ($separatorIndex -lt 1) {
            continue
        }

        $key = $trimmed.Substring(0, $separatorIndex).Trim()
        $value = $trimmed.Substring($separatorIndex + 1).Trim()

        if (($value.StartsWith('"') -and $value.EndsWith('"')) -or ($value.StartsWith("'") -and $value.EndsWith("'"))) {
            $value = $value.Substring(1, $value.Length - 2)
        }

        if ([string]::IsNullOrWhiteSpace([Environment]::GetEnvironmentVariable($key))) {
            [Environment]::SetEnvironmentVariable($key, $value)
        }
    }
}

function Get-EnvOrDefault {
    param(
        [string]$Name,
        [string]$DefaultValue
    )

    $value = [Environment]::GetEnvironmentVariable($Name)
    if ([string]::IsNullOrWhiteSpace($value)) {
        return $DefaultValue
    }

    return $value
}

function Read-BoolEnv {
    param(
        [string]$Name,
        [bool]$DefaultValue = $false
    )

    $value = [Environment]::GetEnvironmentVariable($Name)
    if ([string]::IsNullOrWhiteSpace($value)) {
        return $DefaultValue
    }

    switch ($value.Trim().ToLowerInvariant()) {
        "1" { return $true }
        "true" { return $true }
        "yes" { return $true }
        "on" { return $true }
        "0" { return $false }
        "false" { return $false }
        "no" { return $false }
        "off" { return $false }
        default { return $DefaultValue }
    }
}

function Save-JsonSnapshot {
    param(
        [string]$RelativePath,
        [string]$Uri,
        [hashtable]$Headers = @{}
    )

    $targetPath = Join-Path $RepositoryRoot $RelativePath
    $targetDirectory = Split-Path -Path $targetPath -Parent
    if (-not (Test-Path -LiteralPath $targetDirectory)) {
        New-Item -ItemType Directory -Path $targetDirectory -Force | Out-Null
    }

    $response = Invoke-RestMethod -Method Get -Uri $Uri -Headers $Headers
    $json = $response | ConvertTo-Json -Depth 20
    [System.IO.File]::WriteAllText($targetPath, $json + [Environment]::NewLine, [System.Text.UTF8Encoding]::new($false))

    Write-Host "Saved $RelativePath"
}

function Test-OkxCredentialsConfigured {
    return -not [string]::IsNullOrWhiteSpace([Environment]::GetEnvironmentVariable("OKX_API_KEY")) `
        -and -not [string]::IsNullOrWhiteSpace([Environment]::GetEnvironmentVariable("OKX_API_SECRET")) `
        -and -not [string]::IsNullOrWhiteSpace([Environment]::GetEnvironmentVariable("OKX_API_PASSPHRASE"))
}

function Get-OkxSignedHeaders {
    param(
        [string]$Method,
        [string]$RequestPathAndQuery,
        [hashtable]$Headers = @{}
    )

    if (-not (Test-OkxCredentialsConfigured)) {
        throw "OKX_API_KEY, OKX_API_SECRET, and OKX_API_PASSPHRASE are required to capture authenticated public event-contract snapshots."
    }

    $apiKey = [Environment]::GetEnvironmentVariable("OKX_API_KEY")
    $apiSecret = [Environment]::GetEnvironmentVariable("OKX_API_SECRET")
    $apiPassphrase = [Environment]::GetEnvironmentVariable("OKX_API_PASSPHRASE")
    $timestamp = [DateTime]::UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", [System.Globalization.CultureInfo]::InvariantCulture)
    $payload = $timestamp + $Method.ToUpperInvariant() + $RequestPathAndQuery

    $hmac = [System.Security.Cryptography.HMACSHA256]::new([System.Text.Encoding]::UTF8.GetBytes($apiSecret))
    try {
        $signatureBytes = $hmac.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($payload))
    }
    finally {
        $hmac.Dispose()
    }

    $requestHeaders = @{
        "OK-ACCESS-KEY" = $apiKey
        "OK-ACCESS-SIGN" = [Convert]::ToBase64String($signatureBytes)
        "OK-ACCESS-TIMESTAMP" = $timestamp
        "OK-ACCESS-PASSPHRASE" = $apiPassphrase
    }

    foreach ($key in $Headers.Keys) {
        $requestHeaders[$key] = $Headers[$key]
    }

    return $requestHeaders
}

function Save-AuthenticatedJsonSnapshot {
    param(
        [string]$RelativePath,
        [string]$BaseUrl,
        [string]$RequestPathAndQuery,
        [hashtable]$Headers = @{}
    )

    $signedHeaders = Get-OkxSignedHeaders -Method "GET" -RequestPathAndQuery $RequestPathAndQuery -Headers $Headers
    Save-JsonSnapshot -RelativePath $RelativePath -Uri ($BaseUrl + $RequestPathAndQuery) -Headers $signedHeaders
}

function Save-PublicInstrumentSnapshots {
    param(
        [string]$EnvironmentName,
        [string]$BaseUrl,
        [string]$OptionUnderlying,
        [string]$EventsSeriesId,
        [hashtable]$Headers = @{}
    )

    $root = "OKX.Api.Tests/Fixtures/Live/$EnvironmentName/Public"

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-spot.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=SPOT" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-margin.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=MARGIN" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-swap.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=SWAP" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-futures.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=FUTURES" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-option-btc-usd.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=OPTION&uly=$([Uri]::EscapeDataString($OptionUnderlying))" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-instruments-events-btc-above-daily.json" `
        -Uri "$BaseUrl/api/v5/public/instruments?instType=EVENTS&seriesId=$([Uri]::EscapeDataString($EventsSeriesId))" `
        -Headers $Headers
}

function Resolve-XPerpFundingHistoryInstrumentId {
    param(
        [string]$BaseUrl,
        [string]$OverrideInstrumentId,
        [hashtable]$Headers = @{}
    )

    if (-not [string]::IsNullOrWhiteSpace($OverrideInstrumentId)) {
        return $OverrideInstrumentId
    }

    $instrumentsResponse = Invoke-RestMethod -Method Get -Uri "$BaseUrl/api/v5/public/instruments?instType=FUTURES" -Headers $Headers
    $instrument = $instrumentsResponse.data |
        Where-Object { $_.ruleType -eq "xperp" -and -not [string]::IsNullOrWhiteSpace($_.instId) } |
        Sort-Object instId |
        Select-Object -First 1

    if ($null -eq $instrument) {
        throw "Unable to resolve a live X-Perps futures instrument for funding rate history capture."
    }

    return [string]$instrument.instId
}

function Save-PublicFundingRateSnapshots {
    param(
        [string]$EnvironmentName,
        [string]$BaseUrl,
        [string]$FundingHistoryXPerpInstrumentId,
        [hashtable]$Headers = @{}
    )

    $root = "OKX.Api.Tests/Fixtures/Live/$EnvironmentName/Public"
    $resolvedInstrumentId = Resolve-XPerpFundingHistoryInstrumentId -BaseUrl $BaseUrl -OverrideInstrumentId $FundingHistoryXPerpInstrumentId -Headers $Headers

    Write-Host "Using X-Perps funding history instrument for ${EnvironmentName}: $resolvedInstrumentId"

    Save-JsonSnapshot `
        -RelativePath "$root/get-funding-rate-any.json" `
        -Uri "$BaseUrl/api/v5/public/funding-rate?instId=ANY" `
        -Headers $Headers

    Save-JsonSnapshot `
        -RelativePath "$root/get-funding-rate-history-current-xperp.json" `
        -Uri "$BaseUrl/api/v5/public/funding-rate-history?instId=$([Uri]::EscapeDataString($resolvedInstrumentId))&limit=5" `
        -Headers $Headers
}

function Save-FinancialSnapshots {
    param(
        [string]$EnvironmentName,
        [string]$BaseUrl,
        [string]$BorrowCurrency,
        [hashtable]$Headers = @{}
    )

    $root = "OKX.Api.Tests/Fixtures/Live/$EnvironmentName/Financial"

    Save-JsonSnapshot `
        -RelativePath "$root/get-public-borrow-history-btc.json" `
        -Uri "$BaseUrl/api/v5/finance/savings/lending-rate-history?ccy=$([Uri]::EscapeDataString($BorrowCurrency))&limit=5" `
        -Headers $Headers
}

function Save-EventContractSnapshots {
    param(
        [string]$EnvironmentName,
        [string]$BaseUrl,
        [string]$EventsSeriesId,
        [hashtable]$Headers = @{}
    )

    if (-not (Test-OkxCredentialsConfigured)) {
        Write-Warning "Skipping authenticated event-contract live snapshots because OKX API credentials are not configured."
        return
    }

    $root = "OKX.Api.Tests/Fixtures/Live/$EnvironmentName/Public"
    $escapedSeriesId = [Uri]::EscapeDataString($EventsSeriesId)
    $marketsPathAndQuery = "/api/v5/public/event-contract/markets?seriesId=$escapedSeriesId&limit=20"
    $signedHeaders = Get-OkxSignedHeaders -Method "GET" -RequestPathAndQuery $marketsPathAndQuery -Headers $Headers

    Save-AuthenticatedJsonSnapshot `
        -RelativePath "$root/get-event-contract-series-btc-above-daily.json" `
        -BaseUrl $BaseUrl `
        -RequestPathAndQuery "/api/v5/public/event-contract/series?seriesId=$escapedSeriesId" `
        -Headers $Headers

    Save-AuthenticatedJsonSnapshot `
        -RelativePath "$root/get-event-contract-events-btc-above-daily.json" `
        -BaseUrl $BaseUrl `
        -RequestPathAndQuery "/api/v5/public/event-contract/events?seriesId=$escapedSeriesId&limit=20" `
        -Headers $Headers

    Save-AuthenticatedJsonSnapshot `
        -RelativePath "$root/get-event-contract-markets-btc-above-daily.json" `
        -BaseUrl $BaseUrl `
        -RequestPathAndQuery $marketsPathAndQuery `
        -Headers $Headers

    $marketsResponse = Invoke-RestMethod -Method Get -Uri ($BaseUrl + $marketsPathAndQuery) -Headers $signedHeaders
    $eventInstrumentId = $marketsResponse.data |
        Where-Object { -not [string]::IsNullOrWhiteSpace($_.instId) } |
        Select-Object -ExpandProperty instId -First 1

    if (-not [string]::IsNullOrWhiteSpace($eventInstrumentId)) {
        Save-JsonSnapshot `
            -RelativePath "$root/get-mark-price-events-btc-above-daily.json" `
            -Uri "$BaseUrl/api/v5/public/mark-price?instType=EVENTS&instId=$([Uri]::EscapeDataString($eventInstrumentId))"

        Save-JsonSnapshot `
            -RelativePath "$root/get-open-interest-events-btc-above-daily.json" `
            -Uri "$BaseUrl/api/v5/public/open-interest?instType=EVENTS&instId=$([Uri]::EscapeDataString($eventInstrumentId))"
    }
}

Load-DotEnv -Path (Join-Path $RepositoryRoot ".env")

$baseUrl = Get-EnvOrDefault -Name "OKX_CAPTURE_BASE_URL" -DefaultValue "https://www.okx.com"
$useDemoTrading = Read-BoolEnv -Name "OKX_DEMO_TRADING" -DefaultValue $false
$publicOptionUnderlying = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_OPTION_ULY" -DefaultValue "BTC-USD"
$publicEventsSeriesId = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_EVENTS_SERIES_ID" -DefaultValue "BTC-ABOVE-DAILY"
$borrowCurrency = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_BORROW_CURRENCY" -DefaultValue (Get-EnvOrDefault -Name "OKX_PUBLIC_BORROW_CURRENCY" -DefaultValue "BTC")
$fundingHistoryXPerpInstrumentId = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_FUNDING_HISTORY_XPERP_INST_ID" -DefaultValue ""

Save-PublicInstrumentSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -OptionUnderlying $publicOptionUnderlying `
    -EventsSeriesId $publicEventsSeriesId

Save-PublicFundingRateSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -FundingHistoryXPerpInstrumentId $fundingHistoryXPerpInstrumentId

Save-FinancialSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -BorrowCurrency $borrowCurrency

Save-EventContractSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -EventsSeriesId $publicEventsSeriesId

if ($useDemoTrading) {
    $demoHeaders = @{
        "x-simulated-trading" = "1"
    }

    Save-PublicInstrumentSnapshots `
        -EnvironmentName "Demo" `
        -BaseUrl $baseUrl `
        -OptionUnderlying $publicOptionUnderlying `
        -EventsSeriesId $publicEventsSeriesId `
        -Headers $demoHeaders

    Save-PublicFundingRateSnapshots `
        -EnvironmentName "Demo" `
        -BaseUrl $baseUrl `
        -FundingHistoryXPerpInstrumentId $fundingHistoryXPerpInstrumentId `
        -Headers $demoHeaders

    Save-FinancialSnapshots `
        -EnvironmentName "Demo" `
        -BaseUrl $baseUrl `
        -BorrowCurrency $borrowCurrency `
        -Headers $demoHeaders

    Save-EventContractSnapshots `
        -EnvironmentName "Demo" `
        -BaseUrl $baseUrl `
        -EventsSeriesId $publicEventsSeriesId `
        -Headers $demoHeaders
}
