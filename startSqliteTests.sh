#!/bin/bash
#$1 = port for server to start
#$2 = timeout for patch database
#$3 = thumbprint from cert for server to use (must be already installed)
#$4 = wait after server start in sec
#definiton configfile (have to end with .config: 
#firstline: username
#secondline: userpw
#thirdline: testcasename

serverport=$1
timeout=$2
certthumbprint=$3
teststartdelay=$4

#dont use special characters in config files!

#pathes filenames and urls:
serverconfigfile="appsettings.json"
serverpath="../QS-Torque/bin/server/"
serverpathwindows=$(echo "$serverpath" | tr '/' '\\')
serverconfigtool="ServerConfigTool.exe"
patch="QST7_Patch.exe"
server="gRPC.Service.exe"
databaserepo="https://bitbucket.csp-sw.de/scm/qstng/integrationtestdatabases.git"
patchrepo="https://bitbucket.csp-sw.de/scm/qsttes/v8_dev_patch.git"
testrunnerconfig="../QS-Torque/ServerIntegrationTests/testcasesconfig.json"
configfileextension="*.config"
testsolutionpath="../QS-Torque/"
testsolution="IntegrationTests.sln"
nugetpath="../"
nuget="nuget.exe"
temperrorfile="temperrorfile.txt"
fqdnfile="tempfqdn.txt"
fqdn=""

#script:
rm -rf ./IntegrationTestEnvironment
if [ $? -ge 1 ]; then
  echo "Error while cleaning environment"
  exit 1
fi

mkdir IntegrationTestEnvironment
if [ $? -ge 1 ]; then
  echo "Error while setting up environment"
  exit 1
fi
./writeFQDNtoFile.bat  .\\IntegrationTestEnvironment\\$fqdnfile
cd IntegrationTestEnvironment
fqdn=$(cat "$fqdnfile")
rm -f $fqdnfile

git clone $databaserepo
if [ $? -ge 1 ]; then
  echo "Error while pulling databases"
  exit 1
fi
cd integrationtestdatabases
git checkout master
git pull
cd ..
git clone $patchrepo
if [ $? -ge 1 ]; then
  echo "Error while pulling patch"
  exit 1
fi
cd v8_dev_patch
git checkout master
git pull
cd ..
cp -R ../QS-Torque/bin bin/ 
if [ $? -ge 1 ]; then
  echo "Error while copy server files"
  exit 1
fi

echo "0" > $temperrorfile

writeerrorfile() {
  echo "1" > $temperrorfile
}

setupandstartsqlitetest () {
  echo "testcase found, using config file $0"
  IFS=$'\r\n' GLOBIGNORE='*' command eval  'LINES=($(cat $0))'
  LINES[0]="$(echo "${LINES[0]}"|tr -d '\r\n')"
  LINES[1]="$(echo "${LINES[1]}"|tr -d '\r\n')"
  LINES[2]="$(echo "${LINES[2]}"|tr -d '\r\n')"
  echo '{' > $testrunnerconfig
  echo '  "'${LINES[2]}'": {' >> $testrunnerconfig
  echo '    "username": "'${LINES[0]}'",' >> $testrunnerconfig
  echo '    "password": "'${LINES[1]}'",' >> $testrunnerconfig
  echo '    "serverport": '$serverport',' >> $testrunnerconfig
  echo '    "servername": "'$fqdn'"' >> $testrunnerconfig
  echo '  }' >> $testrunnerconfig
  echo '}' >> $testrunnerconfig
  dbfile="$(dirname "$0")/QST.sqlite"
  windowsdbfile=$(echo "$dbfile" | sed 's/^.\{,1\}//' | sed 's/^\(.\{1\}\)/\1:/' | tr '/' '\\')
  echo "linux dbpath: $dbfile"
  
  echo "using windows dbpath: $windowsdbfile, patching database..."
  timeout $timeout ./v8_dev_patch/$patch dbType="SQLite" dbPath="$windowsdbfile" SilentPatch
  if [ $? -ge 1 ]; then
    writeerrorfile
    echo "Error while patching $windowsdbfile, for $0, skipping..."
	echo ""
	return
  fi
   echo "patching finished"
  
  echo "writing server config..."
  cwd=$(pwd)
  echo $cwd
  cd $serverpath
  ./$serverconfigtool "filepath=$serverconfigfile" "urls=https://*:$serverport" 
  if [ $? -ge 1 ]; then
    cd $cwd
	writeerrorfile
    echo "Error while writing serverconfig urls... skipping"
	echo ""
	return
  fi
  ./$serverconfigtool "filepath=$serverconfigfile" "certthumbprint=$certthumbprint" 
  if [ $? -ge 1 ]; then
    cd $cwd
	writeerrorfile
    echo "Error while writing serverconfig certthumbprint... skipping"
	echo ""
	return
  fi  
  ./$serverconfigtool "filepath=$serverconfigfile" "sqlitefilepath=$windowsdbfile" 
  if [ $? -ge 1 ]; then
    cd $cwd
	writeerrorfile
    echo "Error while writing serverconfig sqlitefilepath... skipping"
	echo ""
	return
  fi  
  echo "starting server..."  
  ./$server &
  serverpid=$(echo "$!") 
  sleep $teststartdelay
  cd $cwd
  ./$nugetpath$nuget restore $testsolutionpath$testsolution
  if [ $? -ge 1 ]; then
    writeerrorfile
    echo "Error while restoring nuget pakages... skipping"
	echo ""
	return
  fi  
  dotnet test $testsolutionpath$testsolution
  if [ $? -ge 1 ]; then
    writeerrorfile
    echo "Error while testing... skipping"
	echo ""
  fi    
  echo "killing server..."
  #kill -2 $serverpid //wuerde prozess wie strg-c beenden - funktioniert aber ned immer
  kill $serverpid
  wait $serverpid  
}
export temperrorfile
export serverport
export timeout
export testrunnerconfig
export serverpath
export serverpathwindows
export serverconfigtool
export patch
export server
export certthumbprint
export teststartdelay
export testsolutionpath
export testsolution
export nugetpath
export nuget
export -f writeerrorfile
export fqdn
export serverconfigfile
export -f setupandstartsqlitetest
find ~+/integrationtestdatabases/SQLite/ -name $configfileextension  -exec bash -c setupandstartsqlitetest {} \;
unset setupandstartsqlitetest
unset serverconfigfile
unset fqdn
unset writeerrorfile
unset nuget
unset nugetpath
unset testsolution
unset testsolutionpath
unset teststartdelay
unset certthumbprint
unset server
unset patch
unset serverconfigtool
unset serverpathwindows
unset serverpath
unset testrunnerconfig
unset timeout
unset serverport
final_exit_status=$(cat "$temperrorfile")
rm -f $temperrorfile
unset temperrorfile
cd ..
rm -rf ./IntegrationTestEnvironment
exit $final_exit_status
