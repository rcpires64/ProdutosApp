using ProdutosApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProdutosApp.Data
{
    public class ProdutosAppContext : IdentityDbContext<Usuario>
    {
        private readonly IConfiguration _configuration;

        public ProdutosAppContext(DbContextOptions<ProdutosAppContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Produto> Produtos { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbSettings = _configuration.GetSection("DatabaseSettings");

            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = dbSettings["Server"],
                InitialCatalog = dbSettings["Database"],
                IntegratedSecurity = bool.Parse(dbSettings["TrustedConnection"]!),
                MultipleActiveResultSets = bool.Parse(dbSettings["MultipleActiveResultSets"]!)
            };

            optionsBuilder.UseSqlServer(connectionString.ToString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Conversor para DateOnly
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                convertToProviderExpression: d => d.ToDateTime(TimeOnly.MinValue),
                convertFromProviderExpression: d => DateOnly.FromDateTime(d));

            // Conversor para DateOnly? (nullable)
            var dateOnlyNullableConverter = new ValueConverter<DateOnly?, DateTime?>(
                convertToProviderExpression: d => d.HasValue ? d.Value.ToDateTime(TimeOnly.MinValue) : null,
                convertFromProviderExpression: d => d.HasValue ? DateOnly.FromDateTime(d.Value) : (DateOnly?)null);


        }
    }
}
