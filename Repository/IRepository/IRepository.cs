using System.Linq.Expressions;

namespace JardeuxBlogV1.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                              string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);



    }
}
