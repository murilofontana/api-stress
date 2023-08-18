
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new PessoaMap());
    }
  }
}
