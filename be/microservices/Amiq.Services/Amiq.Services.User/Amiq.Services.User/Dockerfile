#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Amiq.Services.User/Amiq.Services.User.csproj", "Amiq.Services.User/"]
RUN dotnet restore "Amiq.Services.User/Amiq.Services.User.csproj"
COPY . .
WORKDIR "/src/Amiq.Services.User"
RUN dotnet build "Amiq.Services.User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.Services.User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.Services.User.dll"]