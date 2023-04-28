global using Desafio.Infra.Models;
global using Desafio.Infra;
global using Desafio.Infra.Repository;


namespace Desafio.Service;
public class AccountTransactionService : DesafioService<AccountTransaction>
{

   public AccountTransactionService(IRepository<AccountTransaction> accountTransactionRepo,IUnitOfWork uow):base(accountTransactionRepo,uow)
   {
   }

}
