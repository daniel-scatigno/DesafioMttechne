using Desafio.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Infra.Repository;
using Desafio.Infra.Models;
using Desafio.Services;

namespace Desafio.Test
{
   public class ContextProvider
   {
      public DesafioContext DesafioContext { get; set; }

      public ServiceProvider ServiceProvider { get; private set; }

      public ContextProvider()
      {

         var serviceCollection = new ServiceCollection();


         serviceCollection.AddDbContext<DesafioContext>(options =>
         {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
         }
         );

         serviceCollection.AddScoped<IRepository<AccountTransaction>>(s => new AccountTransactionRepository(s.GetRequiredService<DesafioContext>()));
         serviceCollection.AddScoped<IUnitOfWork>(s => new DesafioUnitOfWork(s.GetRequiredService<DesafioContext>()));
         serviceCollection.AddScoped<AccountTransactionService>(s =>
            new AccountTransactionService(s.GetRequiredService<IRepository<AccountTransaction>>(), s.GetRequiredService<IUnitOfWork>())
         );



         ServiceProvider = serviceCollection.BuildServiceProvider();
         ServiceProvider.GetService<DesafioContext>().Database.EnsureCreated();

      }
   }
}