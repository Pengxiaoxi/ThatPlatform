#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
# 把目录下的内容都复制到当前目录下
COPY . .
EXPOSE 8000
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY . .
#COPY ["Src/ThatPlatform.Core.Web/ThatPlatform.Core.Web.csproj", "Src/ThatPlatform.Core.Web/"]
#COPY ["Src/ThatPlatform.BaseInfo.Applciation/ThatPlatform.BaseInfo.Applciation.csproj", "Src/ThatPlatform.BaseInfo.Applciation/"]
#COPY ["Src/ThatPlatform.BaseInfo.Domain/ThatPlatform.BaseInfo.Domain.csproj", "Src/ThatPlatform.BaseInfo.Domain/"]
#COPY ["Src/ThatPlatform.Common/ThatPlatform.Common.csproj", "Src/ThatPlatform.Common/"]
#COPY ["Src/ThatPlatform.Common.Infrastructure/ThatPlatform.Infrastructure.csproj", "Src/ThatPlatform.Common.Infrastructure/"]
#COPY ["Src/ThatPlatform.Tools.UtilLibrary/ThatPlatform.Tools.UtilLibrary.csproj", "Src/ThatPlatform.Tools.UtilLibrary/"]
#RUN dotnet restore "Src/ThatPlatform.Core.Web/ThatPlatform.Core.Web.csproj"

#WORKDIR "/src/Src/ThatPlatform.Core.Web"
#RUN dotnet build "ThatPlatform.Core.Web.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "ThatPlatform.Core.Web.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ThatPlatform.Core.Web.dll"]

# 运行镜像入口命令和可执行文件名称
ENTRYPOINT ["dotnet", "ThatPlatform.Core.Web.dll"]