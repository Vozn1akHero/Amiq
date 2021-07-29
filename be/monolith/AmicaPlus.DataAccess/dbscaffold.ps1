$workingDir = $PSScriptRoot;

Write-Host "Project dir= $workingDir";
Write-Host "Creating temp project to run scaffold on";
cd ..
mkdir temp
cd temp
mkdir AmicaPlus.DataAccess
cd AmicaPlus.DataAccess
dotnet new webapi
Write-Host "Done creating temp project";
Write-Host "Scaffolding dbcontext into project";
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet ef dbcontext scaffold "data source=.\SQLEXPRESS;Database=AmicaPlus;MultipleActiveResultSets=True;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --project AmicaPlus.DataAccess.csproj
Write-Host "Done scaffolding dbcontext into temp project";
New-Item -ErrorAction Ignore -ItemType directory -Path "$workingDir/Models";
Move-Item ./Models/* "$workingDir/Models" -force;
Write-Host "Scaffold completed! Starting clean-up";
cd ..
cd ..
Remove-Item ./temp -force -Recurse;
cd $workingDir;
Write-Host "Clean-up completed!";