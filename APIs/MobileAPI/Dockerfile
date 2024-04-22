#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIs/MobileAPI/MobileAPI.csproj", "APIs/MobileAPI/"]
COPY ["APIs/Application/Application.csproj", "APIs/Application/"]
COPY ["APIs/Domain/Domain.csproj", "APIs/Domain/"]
COPY ["APIs/Infrastructure/Infrastructure.csproj", "APIs/Infrastructure/"]
RUN dotnet restore "./APIs/MobileAPI/./MobileAPI.csproj"
COPY . .
WORKDIR "/src/APIs/MobileAPI"
RUN dotnet build "./MobileAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MobileAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MobileAPI.dll"]