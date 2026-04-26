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

function Save-JsonSnapshot {
    param(
        [string]$RelativePath,
        [string]$Uri
    )

    $targetPath = Join-Path $RepositoryRoot $RelativePath
    $targetDirectory = Split-Path -Path $targetPath -Parent
    if (-not (Test-Path -LiteralPath $targetDirectory)) {
        New-Item -ItemType Directory -Path $targetDirectory -Force | Out-Null
    }

    $response = Invoke-RestMethod -Method Get -Uri $Uri
    $json = $response | ConvertTo-Json -Depth 20
    [System.IO.File]::WriteAllText($targetPath, $json + [Environment]::NewLine, [System.Text.UTF8Encoding]::new($false))

    Write-Host "Saved $RelativePath"
}

Load-DotEnv -Path (Join-Path $RepositoryRoot ".env")

$baseUrl = Get-EnvOrDefault -Name "OKX_CAPTURE_BASE_URL" -DefaultValue "https://www.okx.com"
$spotInstrumentId = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_SPOT_INST_ID" -DefaultValue "BTC-USDT"
$commodityInstrumentType = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_COMMODITY_INST_TYPE" -DefaultValue "SWAP"
$commodityInstrumentId = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_COMMODITY_INST_ID" -DefaultValue "XAU-USDT-SWAP"
$borrowCurrency = Get-EnvOrDefault -Name "OKX_CAPTURE_PUBLIC_BORROW_CURRENCY" -DefaultValue (Get-EnvOrDefault -Name "OKX_PUBLIC_BORROW_CURRENCY" -DefaultValue "BTC")

Save-JsonSnapshot `
    -RelativePath "OKX.Api.Tests/Fixtures/Live/Public/get-instruments-btc-usdt.json" `
    -Uri "$baseUrl/api/v5/public/instruments?instType=SPOT&instId=$([Uri]::EscapeDataString($spotInstrumentId))"

Save-JsonSnapshot `
    -RelativePath "OKX.Api.Tests/Fixtures/Live/Public/get-instruments-xau-usdt-swap.json" `
    -Uri "$baseUrl/api/v5/public/instruments?instType=$([Uri]::EscapeDataString($commodityInstrumentType))&instId=$([Uri]::EscapeDataString($commodityInstrumentId))"

Save-JsonSnapshot `
    -RelativePath "OKX.Api.Tests/Fixtures/Live/Financial/get-public-borrow-history-btc.json" `
    -Uri "$baseUrl/api/v5/finance/savings/lending-rate-history?ccy=$([Uri]::EscapeDataString($borrowCurrency))&limit=5"
