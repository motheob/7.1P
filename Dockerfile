# Use the .NET 8.0 SDK image as the build environment (preview version)
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the .NET 8.0 runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "7.1P.dll"]