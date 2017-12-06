#!/bin/bash
set -e
set -u

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
DESTINATION="/opt/armyknife"
SYMLINKPATH="/usr/local/bin/armyknife"
BINARYPATH="/opt/armyknife/Armyknife"

if [ -d "$DESTINATION" ]; then
  echo Removing $DESTINATION
  rm -rf $DESTINATION
  
  echo Creating $DESTINATION
  mkdir $DESTINATION
else
  echo Creating $DESTINATION
  mkdir $DESTINATION
fi

echo Moving contents from $DIR to $DESTINATION
mv $DIR/* $DESTINATION

echo Creating symlink of $BINARYPATH to $SYMLINKPATH
ln -s -f $BINARYPATH $SYMLINKPATH