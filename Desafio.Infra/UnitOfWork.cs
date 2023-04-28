
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Desafio.Infra
{
   public class RentalLibraryUnitOfWork : IUnitOfWork
   {
      private DbContext Context { get; set; }
      protected IDbContextTransaction Transaction { get; set; }

      public RentalLibraryUnitOfWork(DbContext context)
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
