using System.Linq.Expressions;

namespace RecordSetup.Interface.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Register(T entity);

        Task<T> Update(T entity);

        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<IList<T>> GetAll();
        Task<T> Delete(T entity);
        Task<IList<T>> GetByExpression(Expression<Func<T, bool>> expression);
    }
}
