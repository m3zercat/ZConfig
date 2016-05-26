$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'
Write-Host "Root: $root"
$version = [System.Reflection.Assembly]::LoadFile("$root\ZConfigParser\bin\Release\ZConfigParser.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\NuGet\ZConfig.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\nuget\ZConfig.compiled.nuspec

& $root\NuGet\NuGet.exe pack $root\nuget\ZConfig.compiled.nuspec

Push-AppveyorArtifact "$root\ZConfig.$versionStr.nupkg" -DeploymentName "ZConfigNuGetPackage"