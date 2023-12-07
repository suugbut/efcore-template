cls

REM create class libraries
dotnet new classlib -o MyUserSecret
dotnet new classlib -o MyModel
dotnet new classlib -o MyContext
dotnet new classlib -o MyMigrationTarget
dotnet new classlib -o MyMigrationStartupFactory



REM add user-secrets to MyUserSecret
del MyUserSecret\Class1.cs
copy templates\MyUserSecret.Empty.cs MyUserSecret\Empty.cs
dotnet user-secrets --project MyUserSecret init
dotnet user-secrets --project MyUserSecret set "ConnectionStrings:JustForFun" "Data Source=JustForFunDatabase.sqlite"
type templates\appsettings.json | dotnet user-secrets --project MyUserSecret set 
dotnet add MyUserSecret package Microsoft.Extensions.Configuration.UserSecrets


REM MyModel
del MyModel\Class1.cs
mkdir MyModel\Models
copy templates\MyModel.TheModel.cs MyModel\Models\TheModel.cs



REM MyContext
del MyContext\Class1.cs
copy templates\MyContext.TheContext.cs MyContext\TheContext.cs
dotnet add MyContext reference MyModel
dotnet add MyContext package Microsoft.EntityFrameworkCore.SqlServer
dotnet add MyContext package Microsoft.EntityFrameworkCore.Sqlite



REM MyMigrationTarget
del MyMigrationTarget\Class1.cs
copy templates\MyMigrationTarget.Empty.cs MyMigrationTarget\Empty.cs
dotnet add MyMigrationTarget reference MyContext




REM MyMigrationStartupFactory
del MyMigrationStartupFactory\Class1.cs
copy templates\MyMigrationStartupFactory.Startup.cs MyMigrationStartupFactory\Startup.cs
dotnet add MyMigrationStartupFactory reference MyUserSecret
dotnet add MyMigrationStartupFactory reference MyMigrationTarget
dotnet add MyMigrationStartupFactory package Microsoft.EntityFrameworkCore.Design



REM create a solution and add projects
dotnet new sln -n Test
dotnet sln add MyUserSecret
dotnet sln add MyModel
dotnet sln add MyContext
dotnet sln add MyMigrationTarget
dotnet sln add MyMigrationStartupFactory
