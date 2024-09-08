using System.Linq.Expressions;

namespace IdentityServer.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity); 
        Task AddRange(IEnumerable<T> entities);
        void Delete(T entity);  
        void DeleteRange(IEnumerable<T> entities);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
