using ComandaPro.Domain.Interfaces.Entities;
using ComandaPro.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ComandaPro.Infra.Data.Repositories;

public abstract class BaseRepository<TObject, G, TContext> : IBaseRepository<TObject, G>
   where TObject : class, IEntity<G>
   where TContext : DbContext
{
    protected TContext _dataContext;

    public BaseRepository(TContext context)
    {
        _dataContext = context;
    }

    public async Task Insert(TObject obj)
    {
        await _dataContext.Set<TObject>().AddAsync(obj);
        await _dataContext.SaveChangesAsync();
    }

    public async Task Update(TObject obj)
    {
        _dataContext.Entry(obj).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(G id)
    {
        _dataContext.Set<TObject>().Remove(await Select(id));
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IList<TObject>> Select() =>
        await _dataContext.Set<TObject>().ToListAsync();

    public async Task<IList<TObject>> Select(Expression<Func<TObject, bool>> predicate) =>
        await _dataContext.Set<TObject>().Where(predicate).ToListAsync();
    
    public async Task<TObject> Select(G id) =>
        await _dataContext.Set<TObject>().FindAsync(id);

}
