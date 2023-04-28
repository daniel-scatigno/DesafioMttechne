using System;
using System.Collections.Generic;

namespace Desafio.Infra
{
   public interface IUnitOfWork : IDisposable
   {      
      int SaveChanges();      

      void StartTransaction();
      
      void CommitTransaction();

      void RollbackTransaction();

      // IEnumerable<EntityEntry> GetChanges(Func<EntityEntry,bool> func);
      // List<string> GetUpdateChangesLog(EntityEntry entry);
      
   }
}