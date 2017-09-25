$rootFolder = Join-Path -Path $PSScriptRoot ".."
$srcFolder = Join-Path -Path $rootFolder "src"
$mainProjectFile = Join-Path $srcFolder "Armyknife\Armyknife.csproj"
$nsiPath = Join-Path $PSScriptRoot "armyknife.nsi"
$binDir = Join-Path $srcFolder "Armyknife\bin\release\netcoreapp2.0\win10-x64\publish"

$nsisPath = "C:\Program Files (x86)\NSIS\Bin"

. "$PSScriptRoot\functions.ps1"

# Updating path variable
Write-Host "Updating path variable"
$env:PATH = "$env:PATH;$nsisPath"

# Remove all bin and obj folders
Write-Host "Cleaning the solution"
Get-ChildItem $srcFolder -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

# Run unit tests
Write-Host "Running unit tests"
$unitTestProjects = Get-ChildItem -Path $srcFolder -Filter *.Tests.csproj -Recurse
foreach($unitTestProject in $unitTestProjects) {
    Write-Host "Running unit test project $unitTestProject"

    & dotnet restore $unitTestProject.FullName
    Assert-Cmd-Ok

    & dotnet test $unitTestProject.FullName
    Assert-Cmd-Ok
}

# Release package build
Write-Host "Building a release package"

& dotnet restore $mainProjectFile
Assert-Cmd-Ok

Write-Host "Packaging up for Windows"
& dotnet publish $mainProjectFile --configuration=release --runtime=win10-x64
Assert-Cmd-Ok

# Reading version number
Write-Host "Reading version from $mainProjectFile"
[xml]$csproj = Get-Content $mainProjectFile
$propertyGroupNode = $csproj.SelectSingleNode("/Project/PropertyGroup[1]")
$version = [version]$propertyGroupNode.Version
Write-Host "Found version $version"

# Making installer
$env:VersionMajor = $version.Major
$env:VersionMinor = $version.Minor
$env:VersionBuild = $version.Build
$env:BuildOutputBinDirectory = $binDir
$env:BuildOutputDirectory = $binDir
Write-Host "Building installer $nsiPath"
& makensis $nsiPath
Assert-Cmd-Ok