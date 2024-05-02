using System.Linq.Expressions;

namespace ComandaPro.Domain.Interfaces.Repositories;

public interface IBaseRepository<T, G> where T : class
{
    Task Insert(T obj);
    Task Update(T obj);
    Task Delete(G id);
    Task<IList<T>> Select();
    Task<IList<T>> Select(Expression<Func<T, bool>> predicate);
    Task<T> Select(G id);
}