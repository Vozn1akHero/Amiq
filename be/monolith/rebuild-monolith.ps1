docker build -t amiq-webapi -f Amiq.WebApi/Dockerfile .
docker container rm amiq-webapi-app -f
#docker container rm amiq-mssql-db -f
#docker run -d --net=amiq_monolith_network -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=123Dimon!!!" -p 1423:1433 --name amiq-mssql-db -v sqlvolume:/var/opt/mssql mcr.microsoft.com/mssql/server:2019-latest
docker run -d -p 8080:80 --net=amiq_monolith_network --name amiq-webapi-app amiq-webapi

if ($Host.Name -eq "ConsoleHost") {
    Write-Host "Press any key to continue..."
    $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyUp") > $null
}