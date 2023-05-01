using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Desafio.Infra.Models;

namespace Desafio.Infra
{

   public class DesafioContext : DbContext
   {
      public DesafioContext(DbContextOptions options) : base(options)
      {

      }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


         /*Default Data*/
         //Exemplo de dados padrões quando necessário
         //builder.Entity<AccountTransaction>().HasData(new AccountTransaction() { Account = 1, Amount=1000});
         
      }

   }
}