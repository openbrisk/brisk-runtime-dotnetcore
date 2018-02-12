#!/bin/bash

# Restore the dependencies of the function.
if [ -e /openbrisk/$MODULE_NAME.csproj ]
then
    dotnet restore --packages /openbrisk/packages /openbrisk/$MODULE_NAME.csproj
fi

# Build the function.
dotnet publish -c Release -o /openbrisk/out /openbrisk/$MODULE_NAME.csproj

# Run the server.
dotnet OpenBrisk.Runtime.dll