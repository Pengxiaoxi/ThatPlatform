#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ThatPlatform.Ugly.Web/ThatPlatform.Ugly.Web.csproj", "ThatPlatform.Ugly.Web/"]
RUN dotnet restore "ThatPlatform.Ugly.Web/ThatPlatform.Ugly.Web.csproj"
COPY . .
WORKDIR "/src/ThatPlatform.Ugly.Web"
RUN dotnet build "ThatPlatform.Ugly.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThatPlatform.Ugly.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThatPlatform.Ugly.Web.dll"]