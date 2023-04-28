global using Desafio.Infra.Models;
global using Desafio.Infra;
global using Desafio.Infra.Repository;


namespace Desafio.Services;
public class AccountTransactionService : DesafioService<AccountTransaction>
{

   public AccountTransactionService(IRepository<AccountTransaction> accountTransactionRepo, IUnitOfWork uow) : base(accountTransactionRepo, uow)
   {

   }
   
   public AccountTransactionViewModel AddAccountTransaction(AccountTransactionViewModel viewModel)
   {
      var model = (AccountTransaction)DesafioMapper.AutoMap(viewModel);
      //Pega o ultimo saldo
      model.HistoricBalance = GetLastBalance(viewModel.Account);
      if (viewModel.Type==ViewModels.OperationType.Debit)
         model.HistoricBalance += viewModel.Amount;
      else 
         model.HistoricBalance -= viewModel.Amount;

      
      model.Date = DateTime.Now;
      Repo.Add(model);

      return (AccountTransactionViewModel)DesafioMapper.AutoMap(model);
   }

   public decimal GetLastBalance(int account)
   {
      

      return Repo.GetBy(x => x.Account == account).OrderByDescending(x => x.Date).Select(x => x.HistoricBalance).LastOrDefault();
   }

}
