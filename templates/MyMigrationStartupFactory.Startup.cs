using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace MyMigrationStartupFactory;

public class Startup : IDesignTimeDbContextFactory<MyContext.TheContext>
{
    public MyContext.TheContext CreateDbContext(string[] args)
    {
        IConfigurationBuilder icb = new ConfigurationBuilder();
        IConfigurationRoot Configuration = icb
                                            .AddUserSecrets<MyUserSecret.Empty>()
                                            .Build();


        // Accessing a strongly typed configuration object
        IConfigurationSection ics = Configuration.GetSection(nameof(MyConfiguration.TheConfiguration));
        var tc = new MyConfiguration.TheConfiguration();
        ics.Bind(tc);
        Console.WriteLine(tc);


                                            
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
