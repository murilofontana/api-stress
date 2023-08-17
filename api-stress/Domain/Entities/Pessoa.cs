using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class Pessoa
  {
    public Guid Id { get; set; }
    public string Apelido { get; set; }
    public string Nome { get; set; }
    public DateOnly Nascimento { get; set; }
    public IEnumerable<string> Stack { get; set; } = Enumerable.Empty<string>();
  }
}
