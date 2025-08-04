#!/bin/bash

echo "Cleaning previous builds"
dotnet clean

echo "Starting build"
dotnet build --configuration Release

echo "Copying Assets to build directory"
cp -r PostbuildCopy/Public bin/Release/net7.0