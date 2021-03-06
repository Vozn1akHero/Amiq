Set-ExecutionPolicy -ExecutionPolicy Bypass

$workingDir = $PSScriptRoot;

Write-Host "Project dir= $workingDir";
Write-Host "Creating temp project to run scaffold on";
cd ..
mkdir temp
cd temp
mkdir DataAccessLayer
cd DataAccessLayer
dotnet new webapi
Write-Host "Done creating temp project";
Write-Host "Scaffolding dbcontext into project";
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0
mkdir Models
dotnet ef dbcontext scaffold "Server=host.docker.internal,1423;Database=Amiq_Friendship;User Id=sa;Password=123Dimon!!!;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --project Amiq.Services.Friendship.csproj --context AmiqFriendshipContext
Write-Host "Done scaffolding dbcontext into temp project"; 
New-Item -ErrorAction Ignore -ItemType directory -Path "$workingDir/Models";
Move-Item ./Models/* "$workingDir/Models" -force;
Write-Host "Scaffold completed! Starting clean-up";
cd ..
cd ..
Remove-Item ./temp -force -Recurse;
cd $workingDir;
Write-Host "Clean-up completed!";