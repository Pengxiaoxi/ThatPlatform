#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8000

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
# WORKDIR /src
# COPY ["Hosting/Tpf.Auth.Api/Tpf.Auth.Api.csproj", "Hosting/Tpf.Auth.Api/"]
# RUN dotnet restore "./Hosting/Tpf.Auth.Api/./Tpf.Auth.Api.csproj"
# COPY . .
# WORKDIR "/src/Hosting/Tpf.Auth.Api"
# RUN dotnet build "./Tpf.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "./Tpf.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ENV ASPNETCORE_ENVIRONMENT TPFTEST

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tpf.Auth.Api.dll"]