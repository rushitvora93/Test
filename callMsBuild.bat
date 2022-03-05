@echo off
pushd %~dp0
dotnet build-server shutdown
mkdir .\QS-Torque\obj

nuget.exe restore .\QS-Torque\QS-Torque.sln
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)
dotnet build-server shutdown
msbuild .\QS-Torque\QS-Torque.sln -p:Configuration=Release /p:Platform="Any CPU" -t:Clean
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)
dotnet build-server shutdown

msbuild .\QS-Torque\QS-Torque.sln -p:Configuration=Release /p:Platform="Any CPU" /p:AssemblyVersion="%1" /p:VersionPrefix="%1" /p:VersionSuffix="%2+%3"
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)
dotnet build-server shutdown

dotnet test -nodereuse:false --collect "Code Coverage" --settings .\QS-Torque\Testsettings.runsettings --blame-hang --blame-hang-timeout 30seconds --blame-hang-dump-type full --logger "console;verbosity=detailed" ".\QS-Torque\QS-Torque.sln" /property:Platform="Any CPU"
dotnet-coverage merge *.coverage -r -f xml
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim testen!"
	exit /b %errorlevel%
)
dotnet build-server shutdown