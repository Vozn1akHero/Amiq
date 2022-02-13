#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amiq.Static/Amiq.Static.csproj", "Amiq.Static/"]
RUN dotnet restore "Amiq.Static/Amiq.Static.csproj"
COPY . .
WORKDIR "/src/Amiq.Static"
RUN dotnet build "Amiq.Static.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.Static.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.Static.dll"]