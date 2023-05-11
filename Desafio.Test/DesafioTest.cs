using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Test;

public abstract class DesafioTest : IDisposable
{
   protected ContextProvider Context { get; set; }
   protected ServiceProvider ServiceProvider { get; set; }
   public DesafioTest()
   {
      Context = new ContextProvider();
      ServiceProvider = Context.ServiceProvider;

   }

   public void Dispose()
   {
      

   }
}