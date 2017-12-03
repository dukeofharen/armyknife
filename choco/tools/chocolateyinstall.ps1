$ErrorActionPreference = 'Stop';
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$fileLocation = Join-Path $toolsDir 'armyknife_install.exe'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  fileType      = 'exe'
  file          = $fileLocation
  silentArgs    = "/S"
  validExitCodes= @(0)
  softwareName  = 'armyknife*'
}

Install-ChocolateyInstallPackage @packageArgs