using Domain.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public Task Insert(Pessoa pessoa)
    {
      _dataContext.AddAsync(pessoa);
      
      return Task.CompletedTask;
    }
  }
}
