# Image usage only for build process.
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers.
COPY ./src ./
RUN dotnet restore ./OpenBrisk.Runtime/

# Copy everything else and build in release mode.
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image.
# HACK: Need to use aspnetcore-build for now, to be able to use 'dotnet restore'.
FROM microsoft/aspnetcore-build:2.0
WORKDIR /app
COPY --from=build-env /app/src/OpenBrisk.Runtime/out ./
COPY --from=build-env /app/startup.sh ./
RUN chmod +x ./startup.sh

# Run the web api application.
WORKDIR /app
ENTRYPOINT ["./startup.sh"]