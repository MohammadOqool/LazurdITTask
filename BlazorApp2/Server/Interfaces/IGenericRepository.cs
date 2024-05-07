using System.Linq.Expressions;

namespace BlazorApp2.Server.Interfaces
{
    public interface IGenericRepository<T> 
    {
        IEnumerable<T> GetAll();
        T Get(string email);
        T? Add(T entity);
        T? Update(T entity);
        bool Remove(T entity);
    }
}
