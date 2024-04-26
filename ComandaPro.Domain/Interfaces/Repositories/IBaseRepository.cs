namespace ComandaPro.Domain.Interfaces.Repositories;

public interface IBaseRepository<T, G> where T : class
{
    Task Insert(T obj);
    Task Update(T obj);
    Task Delete(G id);
    Task<IList<T>> Select();
    Task<T> Select(G id);
}