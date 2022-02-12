FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./ .
RUN dotnet restore "Amiq.Services.Group/Amiq.Services.Group.csproj"
WORKDIR "/src/Amiq.Services.Group"
RUN dotnet build "Amiq.Services.Group.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amiq.Services.Group.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amiq.Services.Group.dll"]