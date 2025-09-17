using Batch.TransacaoFinanceira.domain.models;
using Microsoft.EntityFrameworkCore;

namespace Batch.TransacaoFinanceira.data.database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}