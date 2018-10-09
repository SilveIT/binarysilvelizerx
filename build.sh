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

cp Build/BinarySilvelizer.Example/netcoreapp2.1/publish/BinarySilvelizerX.Example.dll Build/BinarySilvelizer.Example/
cp Build/BinarySilvelizer.Example/netcoreapp2.1/publish/BinarySilvelizerX.Example.deps.json Build/BinarySilvelizer.Example/
cp Build/BinarySilvelizer.Example/netcoreapp2.1/publish/BinarySilvelizerX.Example.runtimeconfig.json Build/BinarySilvelizer.Example/
cp Build/BinarySilvelizer.Example/netcoreapp2.1/publish/BinarySilvelizerX.Example.pdb Build/BinarySilvelizer.Example/

rm -rf "Build/netstandard2.0"
rm -rf "Build/BinarySilvelizer.Example/netcoreapp2.1"

echo "Success"