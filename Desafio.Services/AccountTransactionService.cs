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
      if (viewModel.Amount == 0)
         return viewModel;
      var model = (AccountTransaction)DesafioMapper.AutoMap(viewModel);
      //Pega o ultimo saldo
      model.HistoricBalance = GetLastBalance(viewModel.Account);
      if (viewModel.Type == ViewModels.OperationType.Debit)
         model.HistoricBalance += viewModel.Amount;
      else
         model.HistoricBalance -= viewModel.Amount;


      model.Date = DateTime.Now;
      Repo.Add(model);
      UOW.SaveChanges();

      return (AccountTransactionViewModel)DesafioMapper.AutoMap(model);
   }

   public decimal GetLastBalance(int account)
   {
      return Repo.GetBy(x => x.Account == account).OrderByDescending(x => x.Date).Select(x => x.HistoricBalance).FirstOrDefault();
   }

   public decimal GetBalanceByDate(int account, DateTime date)
   {
      var grouped = Repo.GetBy(x => x.Account == account && x.Date.Date == date.Date)
         .OrderByDescending(x => x.Date)
         .ToList() //Somente porque no SqLite não da suporte para a soma de decimal, uma alternativa é converter para double, mas perde-se precisão
         .GroupBy(x => x.Type)
         .Select(x => new { Type = x.Key, Value = x.Sum(x => x.Amount) }).ToList();
      return grouped.Where(x => x.Type == Infra.Models.OperationType.Debit).Select(x=>x.Value).DefaultIfEmpty(0).FirstOrDefault() - 
             grouped.Where(x => x.Type == Infra.Models.OperationType.Credit).Select(x=>x.Value).DefaultIfEmpty(0).FirstOrDefault();


   }

   public List<AccountTransactionViewModel> ListByDate(int account, DateTime date)
   {
      return Repo.GetBy(x => x.Account == account && x.Date.Date == date.Date)
         .OrderBy(x => x.Date)
         .ToList()
         .Select(x => (AccountTransactionViewModel)DesafioMapper.AutoMap(x))
         .ToList();

   }

   public void ClearByDate(int account, DateTime date)
   {
      Repo.Delete(Repo.GetBy(x => x.Account == account && x.Date.Date == date.Date));
      UOW.SaveChanges();
   }

}
