#!/bin/bash
set -e
set -u
RELEASEURL="https://api.github.com/repos/dukeofharen/armyknife/releases/latest"
JQURL="https://github.com/stedolan/jq/releases/download/jq-1.5/jq-linux64"
JQPATH="/tmp/jq"
TARPATH="/tmp/armyknife.tar.gz"
TAREXTRACTPATH="/tmp/armyknife_install"
INSTALLPATH="$TAREXTRACTPATH/install.sh"

{
    echo "Running install script for Armyknife"

    # Clean or create install folder
    if [ -d "$TAREXTRACTPATH" ]; then
        echo Removing $TAREXTRACTPATH
        rm -rf $TAREXTRACTPATH

        echo Creating $TAREXTRACTPATH
        mkdir $TAREXTRACTPATH
    else
        echo Creating $TAREXTRACTPATH
        mkdir $TAREXTRACTPATH
    fi

    # Download JQ
    if [ -f "$JQPATH" ]; then
        echo "JQ already exists, skipping this step."
    else
        echo "Downloading JQ to temporary location"
        wget $JQURL -O $JQPATH
        chmod +x $JQPATH
    fi

    # Retrieve GitHub information about latest release
    echo "Checking GitHub for latest release"
    RELEASE="$(wget -qO- $RELEASEURL)"
    
    # Getting version from JSON
    VERSION=$(echo $RELEASE | eval $JQPATH "-r '. | .tag_name'")
    echo "Latest version: $VERSION"

    # Download tarball
    TARURL="https://github.com/dukeofharen/armyknife/releases/download/$VERSION/armyknife.tar.gz"
    echo "Downloading Armyknife from URL $TARURL"
    wget $TARURL -O $TARPATH

    # Unpacking tarball
    echo "Unpacking archive $TARPATH"
    tar -xvzf $TARPATH -C $TAREXTRACTPATH

    # Making sure the install.sh script has the correct line endings
    dos2unix $INSTALLPATH

    # Executing install.sh from extracted archive
    echo "Executing script $INSTALLPATH"
    bash $INSTALLPATH
}