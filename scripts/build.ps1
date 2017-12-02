$ErrorActionPreference = 'Stop'

$rootFolder = Join-Path -Path $PSScriptRoot ".."
$srcFolder = Join-Path -Path $rootFolder "src"
$mainProjectFile = Join-Path $srcFolder "Armyknife\Armyknife.csproj"
$solutionFile = Join-Path -Path $srcFolder "Armyknife.sln"
$nsiPath = Join-Path $PSScriptRoot "armyknife.nsi"
$winBinDir = Join-Path $srcFolder "Armyknife\bin\release\netcoreapp2.0\win10-x64\publish"
$installScriptsPath = Join-Path -Path $PSScriptRoot "installscripts"
$unitTestPath = Join-Path -Path $srcFolder "Armyknife.Tests\Armyknife.Tests.csproj"
$openCoverPath = Join-Path -Path $env:USERPROFILE ".nuget\packages\opencover\4.6.519\tools\OpenCover.Console.exe"
$reportGeneratorPath = Join-Path -Path $env:USERPROFILE  ".nuget\packages\reportgenerator\3.0.2\tools\ReportGenerator.exe"
$openCoverReportPath = Join-Path -Path $rootFolder "opencover"
$openCoverReportFilePath = Join-Path -Path $openCoverReportPath "OpenCover.xml"

$nsisPath = "C:\Program Files (x86)\NSIS\Bin"

. "$PSScriptRoot\functions.ps1"

# Updating path variable
Write-Host "Updating path variable"
$env:PATH = "$env:PATH;$nsisPath"

# Remove all bin and obj folders
Write-Host "Cleaning the solution"
Get-ChildItem $srcFolder -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

# Perform a debug build
& dotnet build $solutionFile /p:DebugType=Full

# Run unit tests and calculate code coverage
if(!(Test-Path $openCoverReportPath))
{
    Write-Host "Creating directory $openCoverReportPath" 
    New-item -ItemType Directory -Path $openCoverReportPath
}
else
{
    Write-Host "Deleting directory $openCoverReportPath" 
    Remove-Item -Path $openCoverReportPath -Recurse -Force

    Write-Host "Creating directory $openCoverReportPath" 
    New-item -ItemType Directory -Path $openCoverReportPath
}

Write-Host "Running unit test project $unitTestPath"
& $openCoverPath -target:"C:/Program Files/dotnet/dotnet.exe" -targetargs:"test $unitTestPath --configuration Debug --no-build" -filter:"+[Armyknife*]* -[*.Tests*]*" -oldStyle -register:user -output:"$openCoverReportFilePath" -returntargetcode:1
Assert-Cmd-Ok
& $reportGeneratorPath -reports:$openCoverReportFilePath -targetdir:$openCoverReportPath -verbosity:Error
Assert-Cmd-Ok

# Release package build
Write-Host "Building a release package"

& dotnet restore $mainProjectFile
Assert-Cmd-Ok

# Reading version number
Write-Host "Reading version from $mainProjectFile"
[xml]$csproj = Get-Content $mainProjectFile
$propertyGroupNode = $csproj.SelectSingleNode("/Project/PropertyGroup[1]")
$version = [version]$propertyGroupNode.Version
Write-Host "Found version $version"

. "$PSScriptRoot\build_windows.ps1"

# Patch documentation
$docGenDllFile = Join-Path -Path $srcFolder "Armyknife.DocGenerator\bin\Debug\netcoreapp2.0\Armyknife.DocGenerator.dll"
Write-Host "Now running $docGenDllFile"
& dotnet $docGenDllFile
Assert-Cmd-Ok