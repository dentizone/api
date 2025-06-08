# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Dentizone.sln", "./"]
COPY ["Dentizone.Presentaion/Dentizone.Presentaion.csproj", "Dentizone.Presentaion/"]
COPY ["Dentizone.Application/Dentizone.Application.csproj", "Dentizone.Application/"]
COPY ["Dentizone.Domain/Dentizone.Domain.csproj", "Dentizone.Domain/"]
COPY ["Dentizone.Infrastructure/Dentizone.Infrastructure.csproj", "Dentizone.Infrastructure/"]

# Restore NuGet packages
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the application
RUN dotnet build "Dentizone.Presentaion/Dentizone.Presentaion.csproj" -c Release -o /app/build
RUN dotnet publish "Dentizone.Presentaion/Dentizone.Presentaion.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port 80
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "Dentizone.Presentaion.dll"] 