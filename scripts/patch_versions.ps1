. "$PSScriptRoot\functions.ps1"

$csprojPath = Join-Path -Path $PSScriptRoot "..\src\Armyknife\Armyknife.csproj"

Write-Host "Reading file '$csprojPath'"
[xml]$csproj = Get-Content $csprojPath
$propertyGroupNode = $csproj.SelectSingleNode("/Project/PropertyGroup[1]")
$version = [version]$propertyGroupNode.Version

Write-Host "Current version number: '$version'"

$versionString = "{0}.{1}.{2}.{3}" -f $version.Major, $version.Minor, $version.Build, $env:APPVEYOR_BUILD_NUMBER

Write-Host "New version number: '$versionString'"

$env:versionString = $versionString

$propertyGroupNode.Version = $versionString
$propertyGroupNode.AssemblyVersion = $versionString
$propertyGroupNode.FileVersion = $versionString

$csproj.Save($csprojPath)