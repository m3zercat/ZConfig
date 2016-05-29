$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'
Write-Host "Root: $root"

& $root\NuGet\NuGet.exe restore $root\ZConfig.sln