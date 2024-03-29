using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Desafio.Infra
{
   public class DesafioUnitOfWork : IUnitOfWork
   {
      private DbContext Context { get; set; }
      protected IDbContextTransaction Transaction { get; set; }

      public DesafioUnitOfWork(DbContext context)
      {
         Context = context;
      }

      public virtual int SaveChanges()
      {
         return Context.SaveChanges();
      }

      public virtual void Dispose()
      {
         Context.Dispose();
      }

      public void StartTransaction()
      {
         Transaction = Context.Database.BeginTransaction();
      }
      public void CommitTransaction()
      {
         Transaction.Commit();
      }

      public void RollbackTransaction()
      {
         if (Transaction != null)
            Transaction.Rollback();
      }
   }
}
