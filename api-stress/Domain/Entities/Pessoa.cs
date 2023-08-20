using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class Pessoa
  {
    public Pessoa(string apelido, string nome, DateOnly nascimento, List<string> stack)
    {
      Id = Guid.NewGuid();
      Apelido = apelido;
      Nome = nome;
      Nascimento = nascimento;
      Stack = stack;
    }

    public Guid Id { get; set; }
    public string Apelido { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateOnly Nascimento { get; set; }
    public List<string> Stack { get; set; } = new List<string>();
  }
}
