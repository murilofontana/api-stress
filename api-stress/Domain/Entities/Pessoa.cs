using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
  public static class PessoaValidator
  {
    public static bool IsValid(this Pessoa pessoa)
    {
      return (pessoa.Apelido.Length <= 32) && (pessoa.Nome.Length <= 100) && (pessoa.Stack.All(t => t.Length <= 32));
    }
  }
}


