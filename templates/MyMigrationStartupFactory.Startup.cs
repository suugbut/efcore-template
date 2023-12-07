using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MyMigrationStartupFactory;

public class Startup : IDesignTimeDbContextFactory<MyContext.TheContext>
{
    public MyContext.TheContext CreateDbContext(string[] args)
    {
        IConfigurationBuilder icb = new ConfigurationBuilder();
        IConfigurationRoot Configuration = icb
                                            .AddUserSecrets<MyUserSecret.Empty>()
                                            .Build();
                                            
        var providerName = "SqlServer";
        // var providerName = "Sqlite";

        string constr = Configuration.GetConnectionString(providerName)!;

        string? assemblyName = typeof(MyMigrationTarget.Empty)
                                        .Assembly
                                        .GetName()
                                        .Name;


        var dcob = new DbContextOptionsBuilder<MyContext.TheContext>();
        
        if (providerName == "SqlServer")
            dcob.UseSqlServer(constr, sdcob => sdcob.MigrationsAssembly(assemblyName));

        if (providerName == "Sqlite")
            dcob.UseSqlite(constr, sdcob => sdcob.MigrationsAssembly(assemblyName));

        return new MyContext.TheContext(dcob.Options);
    }
}
