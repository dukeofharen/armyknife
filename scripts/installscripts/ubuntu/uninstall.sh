#!/bin/bash
set -e
set -u

SYMLINKPATH="/usr/local/bin/armyknife"
BINARYPATH="/opt/armyknife"

rm $SYMLINKPATH
rm -rf $BINARYPATH