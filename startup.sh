#!/bin/bash

if [ -e /openbrisk/$MODULE_NAME.csproj ]
then
    dotnet restore --packages /openbrisk/packages /openbrisk/$MODULE_NAME.csproj
fi

dotnet OpenBrisk.Runtime.dll