#!/bin/bash

if [ -e /openbrisk/$MOD_NAME.csproj ]
then
    dotnet restore --packages /openbrisk/packages /openbrisk/$MOD_NAME.csproj
fi

dotnet OpenBrisk.Runtime.dll