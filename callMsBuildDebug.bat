@echo off
pushd %~dp0
mkdir .\QS-Torque\obj

nuget.exe restore .\QS-Torque\QS-Torque.sln
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)

msbuild .\QS-Torque\QS-Torque.sln -p:Configuration=Debug /p:Platform="Any CPU" -t:Clean
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)

msbuild .\QS-Torque\QS-Torque.sln -p:Configuration=Debug /p:Platform="Any CPU" /p:AssemblyVersion="%1" /p:VersionPrefix="%1" /p:VersionSuffix="%2+%3"
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim compilieren"
	exit /b %errorlevel%
)

dotnet test --collect "Code Coverage" --settings .\QS-Torque\Testsettings.runsettings --logger "console;verbosity=detailed" ".\QS-Torque\QS-Torque.sln" /property:Platform="Any CPU"
dotnet-coverage merge *.coverage -r -f xml
if %ERRORLEVEL% GEQ 1 (
	echo "Fehler beim testen!"
	exit /b %errorlevel%
)