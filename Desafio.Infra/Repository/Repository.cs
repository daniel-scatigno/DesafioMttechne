using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Desafio.Infra.Repository
{
   public abstract class Repository<T> : IRepository<T> where T : class
   {
      protected DbContext Context { get; set; }
      protected DbSet<T> DbSet { get; set; }

      private IQueryable<T> DefaultQuery { get; set; }

      private ILogger<Repository<T>>? Log { get; set; }

      public Repository(DbContext context)
      {
         Context = context;
         DbSet = context.Set<T>();
         DefaultQuery = DbSet.AsQueryable<T>();         
      }
      public Repository(DbContext context, ILogger<Repository<T>> log ) : this(context)
      {
         Log = log;
      }

      public virtual IQueryable<T> GetQueryableById(object id, bool noTracking = false)
      {
         var parameterExpression = Expression.Parameter(typeof(T));
         var property = Expression.Property(parameterExpression, Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault().PropertyInfo);

         //Converte o Id para o tipo da chave primaria do objeto T
         var constant = Expression.Constant(
              property.Type == typeof(int) ? Convert.ToInt32(id)
            : property.Type == typeof(Int64) ? Convert.ToInt64(id)
            : property.Type == typeof(string) ? Convert.ToString(id)
            : id);
         var expression = Expression.Equal(property, constant);
         var lambda = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);

         IQueryable<T> result;

         if (noTracking)
            result = DefaultQuery.Where(lambda).AsNoTracking();
         else
            result = DefaultQuery.Where(lambda);

         return result;

      }

      public virtual T GetById(object id, bool noTracking = false)
      {
         return GetQueryableById(id, noTracking).FirstOrDefault();

      }

      public virtual IQueryable<T> GetAll()
      {
         return DefaultQuery;

      }

      public virtual IQueryable<T> GetBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
      {

         IQueryable<T> query = DefaultQuery.Where(expression);
         return query;

      }


      public virtual void Add(T item)
      {
         DbSet.AddRange(item);
      }

      public virtual void Add(IEnumerable<T> lst)
      {
         DbSet.AddRange(lst);
      }

      public virtual void Update(T entity)
      {
         DbSet.Update(entity);
      }

      public virtual void Update(IEnumerable<T> lst)
      {
         DbSet.UpdateRange(lst);
      }

      public void Delete(T entity)
      {
         DbSet.Remove(entity);

      }

      public void Delete(System.Collections.Generic.IEnumerable<T> entities)
      {

         DbSet.RemoveRange(entities);

      }



   }

}
