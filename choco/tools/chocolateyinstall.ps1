$ErrorActionPreference = 'Stop';
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$fileLocation = Join-Path $toolsDir 'armyknife_install.exe'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  fileType      = 'exe'
  file          = $fileLocation

  softwareName  = 'armyknife*'

  checksum      = '[CHECKSUM]'
  checksumType  = 'sha256'

  silentArgs    = '/S'

  validExitCodes= @(0)
}

Install-ChocolateyPackage @packageArgs