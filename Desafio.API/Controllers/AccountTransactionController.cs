using Desafio.Infra.Models;
using Desafio.Services;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountTransactionController : DesafioController<AccountTransactionViewModel, AccountTransaction>
{
   public AccountTransactionService AccountTransactionService => (AccountTransactionService)Service;
   public AccountTransactionController(AccountTransactionService service) : base(service)
   {

   }
   [HttpGet]
   public List<AccountTransactionViewModel> ListTransaction(int account, DateTime date)
   {
      return AccountTransactionService.ListByDate(account, date);
   }

   [HttpPost]
   public AccountTransactionViewModel AddDebit(AccountTransactionViewModel viewModel)
   {
      viewModel.Type = ViewModels.OperationType.Debit;
      viewModel = AccountTransactionService.AddAccountTransaction(viewModel); //Atualiza as informações caso elas tenhas sido atualizadas pelo serviço
      return viewModel;
   }

   [HttpPost]
   public AccountTransactionViewModel AddCredit(AccountTransactionViewModel viewModel)
   {
      viewModel.Type = ViewModels.OperationType.Credit;
      viewModel = AccountTransactionService.AddAccountTransaction(viewModel); //Atualiza as informações caso elas tenhas sido atualizadas pelo serviço
      return viewModel;
   }

   [HttpGet]
   public decimal GetBalance(int account, DateTime date)
   {
      return AccountTransactionService.GetLastBalance(account,date);
   }




}
