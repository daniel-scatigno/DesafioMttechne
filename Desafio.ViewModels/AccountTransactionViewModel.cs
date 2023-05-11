global using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Desafio.ViewModels;
public class AccountTransactionViewModel
{
   public int Account { get; set; }

   public decimal Amount { get; set; }

   public DateTime Date { get; set;}

   public OperationType Type { get; set; }

   public decimal HistoricBalance { get;set; }

}
public enum OperationType
{
   [Display(Name = "Débito")] Debit,
   [Display(Name = "Crédito")] Credit
}

public class AccountTransactionViewModelValidation : FluentValidation.AbstractValidator<AccountTransactionViewModel>
{
   public AccountTransactionViewModelValidation()
   {
      RuleSet("Default",() =>
      {
         RuleFor(x => x.Amount).NotEmpty();
         RuleFor(x => x.Amount).Must(x=>x >= 0).WithMessage("A quantidade não pode ser negativa");
         RuleFor(x => x.Account).NotEmpty();
         
      });
      
   }
}

//Débito = Entrada de Valores, Crédito = Saída de Valores