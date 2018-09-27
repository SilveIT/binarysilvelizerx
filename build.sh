#!/bin/sh

echo "Check or Create Directory..."
if [ -d "Build" ]; then
	rm -rf "Build"
fi

cd BinarySilvelizerX

dotnet build --configuration Release
dotnet publish

cd ..

cp Build/netstandard2.0/publish/BinarySilvelizerX.dll Build/
cp Build/netstandard2.0/publish/BinarySilvelizerX.deps.json Build/
cp Build/netstandard2.0/publish/BinarySilvelizerX.pdb Build/

rm -rf "Build/netstandard2.0"

echo "Success"