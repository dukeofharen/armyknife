$ErrorActionPreference = 'Stop'

$rootFolder = Join-Path -Path $PSScriptRoot ".."
$chocoFolder = Join-Path -Path $rootFolder "choco"
$toolsFolder = Join-Path -Path $chocoFolder "tools"
$srcFolder = Join-Path -Path $rootFolder "src"

$installerPath = Join-Path -Path $srcFolder "Armyknife\bin\release\netcoreapp2.0\win10-x64\publish\armyknife_install.exe"
$verificationPath = Join-Path -Path $toolsFolder "VERIFICATION.txt"
$chocoInstallPath = Join-Path -Path $toolsFolder "chocolateyinstall.ps1"
$nuspecPath = Join-Path -Path $chocoFolder "armyknife.nuspec"
$mainProjectFile = Join-Path $srcFolder "Armyknife\Armyknife.csproj"

. "$PSScriptRoot\functions.ps1"

if(!(Test-Path $installerPath))
{
    throw "Installer path not found: $installerPath"
}

if(!(Test-Path $verificationPath))
{
    throw "Installer path not found: $verificationPath"
}

if(!(Test-Path $chocoInstallPath))
{
    throw "Installer path not found: $chocoInstallPath"
}

if(!(Test-Path $nuspecPath))
{
    throw "Installer path not found: $nuspecPath"
}

if(!(Test-Path $mainProjectFile))
{
    throw "Installer path not found: $mainProjectFile"
}

Write-Host "Copying installer from $installerPath to $toolsFolder"
Copy-Item $installerPath $toolsFolder

$hash = (Get-FileHash -Algorithm SHA256 $installerPath).Hash
Write-Host "Calculated SHA256 hash of installer file: $hash"

$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False

Write-Host "Patching generated installer hash to $verificationPath"
$verification = (Get-Content $verificationPath).Replace("[CHECKSUM]", $hash)
[System.IO.File]::WriteAllLines($verificationPath, $verification, $Utf8NoBomEncoding)

Write-Host "Patching generated installer hash to $chocoInstallPath"
$chocoInstall = (Get-Content $chocoInstallPath).Replace("[CHECKSUM]", $hash)
[System.IO.File]::WriteAllLines($chocoInstallPath, $chocoInstall, $Utf8NoBomEncoding)

Write-Host "Reading version from $mainProjectFile"
[xml]$csproj = Get-Content $mainProjectFile
$propertyGroupNode = $csproj.SelectSingleNode("/Project/PropertyGroup[1]")
$version = [version]$propertyGroupNode.Version
Write-Host "Found version $version"

Write-Host "Patching version to $nuspecPath"
$nuspec = (Get-Content $nuspecPath).Replace("[VERSION]", $version)
[System.IO.File]::WriteAllLines($nuspecPath, $nuspec, $Utf8NoBomEncoding)

Write-Host "Building Chocolatey packages"
& choco pack $nuspecPath
Assert-Cmd-Ok