#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
ROOTFOLDER="$DIR/.."

echo Deleting al bin and obj folders
rm -rf `find $ROOTFOLDER -type d -name bin`
rm -rf `find $ROOTFOLDER -type d -name obj`