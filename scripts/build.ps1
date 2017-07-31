$rootFolder = Join-Path -Path $PSScriptRoot ".."
$srcFolder = Join-Path -Path $rootFolder "src"
$mainProjectFile = Join-Path $srcFolder "Armyknife\Armyknife.csproj"

. "$PSScriptRoot\functions.ps1"

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