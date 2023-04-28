using Desafio.Infra;
using Desafio.Infra.Models;
using Desafio.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Services;
using Desafio.ViewModels;

namespace Desafio.Test;

public class TransactionTest : DesafioTest
{
   public TransactionTest() : base()
   {
   }

   [Theory]
   [InlineData(1)]
   public void ShouldIncludeDebit(int account)
   {
      var uow = ServiceProvider.GetService<IUnitOfWork>();
      var accountTransactionRepo = ServiceProvider.GetService<IRepository<AccountTransaction>>();
      var accountTransactionService = ServiceProvider.GetRequiredService<AccountTransactionService>();
      
      accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
      {
         Account = account,
         Amount = 1000,
         Type = ViewModels.OperationType.Debit
      });

      accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
      {
         Account = account,
         Amount = 1200,
         Type = ViewModels.OperationType.Debit
      });
      var lastBalance = accountTransactionService.GetLastBalance(account);

      Assert.True(lastBalance == 2200);

   }
}