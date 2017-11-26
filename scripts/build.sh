#!/bin/bash
set -e
set -u
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
ROOTFOLDER="$DIR/.."
SRCFOLDER="$ROOTFOLDER/src"
SLNPATH="$SRCFOLDER/Armyknife.sln"
UNITTESTCSPROJPATH="$SRCFOLDER/Armyknife.Tests/Armyknife.Tests.csproj"
MAINPROJECTFILE="$SRCFOLDER/Armyknife/Armyknife.csproj"

echo Deleting all bin and obj folders
rm -rf `find $ROOTFOLDER -type d -name bin`
rm -rf `find $ROOTFOLDER -type d -name obj`

echo Building $SLNPATH
dotnet restore $SLNPATH
dotnet build $SLNPATH /p:DebugType=Full

echo Running unit tests in project $UNITTESTCSPROJPATH
dotnet test $UNITTESTCSPROJPATH --configuration Debug --no-build

echo Building Ubuntu release package
dotnet publish $MAINPROJECTFILE --configuration=release --runtime=ubuntu.16.04-x64