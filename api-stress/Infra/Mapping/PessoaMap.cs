using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
  public class PessoaMap : IEntityTypeConfiguration<Pessoa>
  {
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
      builder.HasKey(t => t.Id);

      builder.Property(t => t.Apelido).IsRequired().HasMaxLength(32);

      builder.Property(t => t.Nome).IsRequired().HasMaxLength(100);

      builder.Property(t => t.Nascimento).IsRequired();

      builder.Property(t => t.Stack).HasMaxLength(32);

      builder.HasIndex(t => t.Apelido).IsUnique();
    }
  }
}
