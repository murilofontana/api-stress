using Domain.Interfaces.Repositories;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace api_stress.Extensions
{
  public static class DatabaseExtension
  {

    public static void MigrateDatabase(
       this IApplicationBuilder builder)
    {
      using (var scope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
      }
    }
  }
}
