# Image usage only for build process.
FROM microsoft/aspnetcore-build:2.1 AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers.
COPY ./src/OpenBrisk.Runtime/OpenBrisk.Runtime.csproj ./OpenBrisk.Runtime/OpenBrisk.Runtime.csproj
COPY ./src/OpenBrisk.Runtime.sln ./OpenBrisk.Runtime.sln
RUN dotnet restore

# Copy everything else and build in release mode.
COPY ./src .
RUN dotnet publish -c Release -o out

# Build runtime image.
# HACK: Need to use aspnetcore-build for now, to be able to use 'dotnet restore'.
# => Maybe just add the nuget.exe and use it for the restore?
FROM microsoft/aspnetcore-build:2.1
WORKDIR /app
COPY --from=builder /src/OpenBrisk.Runtime/out .
COPY startup.sh .
RUN chmod +x ./startup.sh

ENTRYPOINT ["./startup.sh"]