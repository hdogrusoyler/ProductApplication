using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Product.Application.DataAccess.EntityFramework;

namespace Product.Application.Presentation.Helper
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DataConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Product.Application.Presentation"));
            return new DataContext(builder.Options);
        }
    }
}
