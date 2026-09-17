using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Supportly.BusinessObjects;

public class SupportlyDbContextFactory
    : IDesignTimeDbContextFactory<SupportlyDbContext>
{
    public SupportlyDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "../Supportly.API"
        );

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json",
                optional: true
            )
            .AddUserSecrets<SupportlyDbContext>(optional: true)
            .Build();

        var connectionString =
            configuration.GetConnectionString("Supportly");

        var optionsBuilder =
            new DbContextOptionsBuilder<SupportlyDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new SupportlyDbContext(optionsBuilder.Options);
    }
}