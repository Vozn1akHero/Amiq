#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amiq.Services.Notification/Amiq.Services.Notification.csproj", "Amiq.Services.Notification/"]
RUN dotnet restore "Amiq.Services.Notification/Amiq.Services.Notification.csproj"
COPY . .
WORKDIR "/src/Amiq.Services.Notification"
RUN dotnet build "Amiq.Services.Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.Services.Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.Services.Notification.dll"]