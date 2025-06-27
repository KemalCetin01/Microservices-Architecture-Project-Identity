using Microsoft.Extensions.Configuration;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Helpers;

public class IdentityDbContextOption
{
    private readonly IConfiguration _configuration;

    public IdentityDbContextOption(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbContextOptions<IdentityDbContext> GetIdentityDbContextOption()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName));
        return dbContextOptionsBuilder.Options;
    }
}
