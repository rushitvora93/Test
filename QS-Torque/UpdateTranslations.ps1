param (
	[String]$solutionPath,
	[String]$targetPath
)

Write $solutionPath
cd $solutionPath

$gettext = "$env:USERPROFILE\.nuget\packages\gettext.tools\0.21.0\tools\bin\xgettext.exe"
$gettextcat = "$env:USERPROFILE\.nuget\packages\gettext.tools\0.21.0\tools\bin\msgcat.exe"
$gettextmerge = "$env:USERPROFILE\.nuget\packages\gettext.tools\0.21.0\tools\bin\msgmerge.exe"
$gettextfmt = "$env:USERPROFILE\.nuget\packages\gettext.tools\0.21.0\tools\bin\msgfmt.exe"
Write $gettext

$gettextwpf = "$env:USERPROFILE\.nuget\packages\ngettext.wpf\1.2.4\tools\XGetText-Xaml.ps1"
Write $gettextwpf


write Get-ChildItem -Path $testpath -Recurse -File -Filter *.xaml | Where { $_.FullName -NotLike '*\obj\*' } | ForEach-Object { $_.FullName }

. $gettextwpf


XGetText-Xaml -o obj/xamlmessages.pot -k Gettext,GettextFormatConverter @(Get-ChildItem -Path "$solutionPath" -Recurse -File -Filter *.xaml | Where { $_.FullName -NotLike '*\obj\*' } | ForEach-Object { $_.FullName })

Get-ChildItem -Path "$solutionPath" -Recurse -File -Filter *.cs | Where { $_.FullName -NotLike '*\obj\*' } | ForEach-Object { $_.FullName } | Out-File -Encoding ascii "obj\csharpfiles"
& $gettext --force-po --from-code UTF-8 --language=c# -o obj\csmessages.pot -k_ -kNoop:1g -kEnumMsgId:1g --keyword=Catalog.GetString --keyword=PluralGettext:2,3 --files-from=obj\csharpfiles --no-location

& $gettextcat --use-first -o obj/result.pot obj/csmessages.pot obj/xamlmessages.pot

$locales = @("en-US", "de-DE")
$poFiles = $($locales | ForEach-Object { "$solutionPath/Locale/" + $_ + "/LC_MESSAGES/Messages.po" })
$poFiles | ForEach-Object { & $gettextmerge --sort-output --update $_ obj/result.pot }

Write $targetPath
Copy-Item -Path "$solutionPath/Locale/" -Recurse -Destination $targetPath -Container -force
$finalPoFolders = $($locales | ForEach-Object { "${targetPath}Locale\" + $_ + "\LC_MESSAGES" })
$finalPoFolders | ForEach-Object { & $gettextfmt -o "$_\Messages.mo" "$_\Messages.po"}
