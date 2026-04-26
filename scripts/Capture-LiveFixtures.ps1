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

Load-DotEnv -Path (Join-Path $RepositoryRoot ".env")

$baseUrl = Get-EnvOrDefault -Name "OKX_CAPTURE_BASE_URL" -DefaultValue "https://www.okx.com"
$useDemoTrading = Read-BoolEnv -Name "OKX_DEMO_TRADING" -DefaultValue $false
$publicOptionUnderlying = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_OPTION_ULY" -DefaultValue "BTC-USD"
$publicEventsSeriesId = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_EVENTS_SERIES_ID" -DefaultValue "BTC-ABOVE-DAILY"
$borrowCurrency = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_BORROW_CURRENCY" -DefaultValue (Get-EnvOrDefault -Name "OKX_PUBLIC_BORROW_CURRENCY" -DefaultValue "BTC")

Save-PublicInstrumentSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -OptionUnderlying $publicOptionUnderlying `
    -EventsSeriesId $publicEventsSeriesId

Save-FinancialSnapshots `
    -EnvironmentName "Production" `
    -BaseUrl $baseUrl `
    -BorrowCurrency $borrowCurrency

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

    Save-FinancialSnapshots `
        -EnvironmentName "Demo" `
        -BaseUrl $baseUrl `
        -BorrowCurrency $borrowCurrency `
        -Headers $demoHeaders
}
