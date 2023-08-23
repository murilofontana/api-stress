using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Infra.Repositories
{
  public class PessoaRepository : IPessoaRepository
  {
    private DataContext _dataContext;

    public PessoaRepository(DataContext dataContext)
    {
      this._dataContext = dataContext;
    }

    public async Task<int> CountPessoas()
    {
      var set = _dataContext.Set<Pessoa>();
      return await set.CountAsync();
    }

    public async Task<Pessoa> Get(Guid id) => await _dataContext.FindAsync<Pessoa>(id);

    public async Task Insert(Pessoa pessoa)
    {

      var set = _dataContext.Set<Pessoa>();
      await set.AddAsync(pessoa);      

      await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Pessoa>> SearchByTerm(string term)
    {
      var set = _dataContext.Set<Pessoa>().AsQueryable();

      var filtedData = set.Where(t => t.Nome.Contains(term) || t.Apelido.Contains(term)).Take(50);

      return await filtedData.ToListAsync();
    }
  }
}
