FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./ .
RUN dotnet restore "Amiq.ApiGateways.WebApp/Amiq.ApiGateways.WebApp.csproj"
WORKDIR "/src/Amiq.ApiGateways.WebApp"
RUN dotnet build "Amiq.ApiGateways.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.ApiGateways.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.ApiGateways.WebApp.dll"]