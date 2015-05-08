#!/bin/bash


if ps ax | grep -v grep | grep 'Unity$' > /dev/null 
then
    kill $(ps aux | grep 'Unity$' | awk '{print $2}')
fi
UNITY_PATH="${1}"
cd $UNITY_PATH
rm -rf Library
PLATFORM_DIR="Library_${2}"
if [ ! -d $PLATFORM_DIR ]; then
  mkdir $PLATFORM_DIR
fi
ln -s $PLATFORM_DIR Library
open -n "${3}"
