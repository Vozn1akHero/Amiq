FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Amiq.WebApi/Amiq.WebApi.csproj"
WORKDIR "/src/Amiq.WebApi"
RUN dotnet build "Amiq.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.WebApi.dll"]