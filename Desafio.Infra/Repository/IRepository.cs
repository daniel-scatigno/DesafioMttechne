using System.Linq.Expressions;

namespace Desafio.Infra.Repository;
public interface IRepository<T> where T : class
{

   IQueryable<T> GetAll();
   IQueryable<T> GetBy(Expression<Func<T, bool>> expression);

   T GetById(object id, bool noTracking = false);

   IQueryable<T> GetQueryableById(object id, bool noTracking = false);

   void Add(T entity);
   void Add(IEnumerable<T> items);
   
   void Update(T entity);
   void Delete(T entity);
   void Delete(IEnumerable<T> entities);   
   
}