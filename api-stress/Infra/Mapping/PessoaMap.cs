using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapping
{
  public class PessoaMap : IEntityTypeConfiguration<Pessoa>
  {
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
      builder.HasKey(t => t.Id);
    }
  }
}
