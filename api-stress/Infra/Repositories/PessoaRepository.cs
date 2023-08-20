using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
  public class PessoaRepository : IPessoaRepository
  {
    private DataContext _dataContext;

    public PessoaRepository(DataContext dataContext)
    {
      this._dataContext = dataContext;
    }

    public async Task<Pessoa> Get(Guid id) => await _dataContext.FindAsync<Pessoa>(id);

    public async Task Insert(Pessoa pessoa)
    {
      await _dataContext.AddAsync(pessoa);

      await _dataContext.SaveChangesAsync();
      
    }

    public async Task<IEnumerable<Pessoa>> SearchByTerm(string term)
    {
      var set = _dataContext.Set<Pessoa>();

      var filtedData = set.Where(t => t.Nome.Contains(term) || t.Apelido.Contains(term) || t.Stack.Contains(term));

      return await filtedData.ToListAsync();
    }
  }
}
