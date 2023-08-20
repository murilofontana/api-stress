namespace api_stress.DTOs.Request
{
  public class PessoaRequest
  {
    public string Apelido { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateOnly Nascimento { get; set; }
    public List<string> Stack { get; set; } = new List<string>();

  }
}
