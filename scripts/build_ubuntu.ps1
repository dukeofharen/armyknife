Param(
    [Parameter(Mandatory = $True)]
    [string]$srcFolder
)

$ErrorActionPreference = 'Stop'

. "$PSScriptRoot\functions.ps1"

$binDir = Join-Path $srcFolder "Armyknife\bin\release\netcoreapp2.0\ubuntu.16.04-x64\publish"
$installScriptsPath = Join-Path -Path $PSScriptRoot "installscripts\ubuntu"

$env:PATH = "$($env:PATH);C:\Program Files\7-Zip"

# Create Ubuntu package
Write-Host "Packing up for Ubuntu" -ForegroundColor Green
& dotnet publish $mainProjectFile --configuration=release --runtime=ubuntu.16.04-x64
Assert-Cmd-Ok

# Moving install scripts for Windows
Copy-Item (Join-Path $installScriptsPath "**") $binDir -Recurse

# Creating .tar.gz file of binaries.
& 7z a -ttar "$binDir\armyknife.tar" "$binDir\*.*"
Assert-Cmd-Ok
& 7z a -tgzip "$binDir\armyknife.tar.gz" "$binDir\armyknife.tar"
Assert-Cmd-Ok
Remove-Item "$binDir\armyknife.tar"