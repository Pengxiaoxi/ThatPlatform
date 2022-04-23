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
#COPY ["Src/Tpf.Core.Web/Tpf.Core.Web.csproj", "Src/Tpf.Core.Web/"]
#COPY ["Src/Tpf.BaseInfo.Applciation/Tpf.BaseInfo.Applciation.csproj", "Src/Tpf.BaseInfo.Applciation/"]
#COPY ["Src/Tpf.BaseInfo.Domain/Tpf.BaseInfo.Domain.csproj", "Src/Tpf.BaseInfo.Domain/"]
#COPY ["Src/Tpf.Common/Tpf.Common.csproj", "Src/Tpf.Common/"]
#COPY ["Src/Tpf.Common.Infrastructure/Tpf.Infrastructure.csproj", "Src/Tpf.Common.Infrastructure/"]
#COPY ["Src/Tpf.Utils/Tpf.Utils.csproj", "Src/Tpf.Utils/"]
#RUN dotnet restore "Src/Tpf.Core.Web/Tpf.Core.Web.csproj"

#WORKDIR "/src/Src/Tpf.Core.Web"
#RUN dotnet build "Tpf.Core.Web.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "Tpf.Core.Web.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Tpf.Core.Web.dll"]

# 运行镜像入口命令和可执行文件名称
ENTRYPOINT ["dotnet", "Tpf.Core.Web.dll"]