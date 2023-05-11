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

   [Theory]
   [InlineData(1)]
   public void ShouldIncludeCredit(int account)
   {
      var accountTransactionService = ServiceProvider.GetRequiredService<AccountTransactionService>();


      accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
      {
         Account = account,
         Amount = 1000,
         Type = ViewModels.OperationType.Debit
      });

      var balance = accountTransactionService.GetLastBalance(account);

      accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
      {
         Account = account,
         Amount = 200,
         Type = ViewModels.OperationType.Credit
      });
      var lastBalance = accountTransactionService.GetLastBalance(account);

      Assert.True(lastBalance == balance - 200);

   }

   [Theory]
   [InlineData(1)]
   public void ShouldFailToAddIncorrectValue(int account)
   {
      var accountTransactionService = ServiceProvider.GetRequiredService<AccountTransactionService>();

      Assert.Throws<Exception>(() =>
      {
         accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
         {
            Account = account,
            Amount = 0,
            Type = ViewModels.OperationType.Debit
         });
      });

      Assert.Throws<Exception>(() =>
      {
         accountTransactionService.AddAccountTransaction(new AccountTransactionViewModel()
         {
            Account = account,
            Amount = -1,
            Type = ViewModels.OperationType.Credit
         });
      });

   }

   [Theory]
   [InlineData(1)]
   public void ShouldListTransactionsInDate(int account)
   {
      ShouldIncludeDebit(account);
      ShouldIncludeCredit(account);

      var accountTransactionService = ServiceProvider.GetRequiredService<AccountTransactionService>();

      var list = accountTransactionService.ListByDate(account, DateTime.Today);

      Assert.True(list.Count() > 0);
      list.ForEach(t =>
      {
         Console.WriteLine("Operação de " + t.Type.ToString() + " valor:" + t.Amount + " saldo:" + t.HistoricBalance);
      });


   }
}