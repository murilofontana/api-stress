using Domain.Interfaces.Repositories;
using Infra.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api_stress.Extensions
{
  public static class RepositoryExtension
  {
    public static void AddRepositories(
           this IServiceCollection services)
    {
      services.AddTransient<IPessoaRepository, PessoaRepository>();
    }
  }
}
