global using System.ComponentModel.DataAnnotations;

namespace Desafio.ViewModels;
public class AccountTransactionViewModel
{
   public int Account { get; set; }

   public decimal Amount { get; set; }

   public DateTime Date { get; }

   public OperationType Type { get; set; }

   public decimal HistoricBalance { get; }

}
public enum OperationType
{
   [Display(Name = "Débito")] Debit,
   [Display(Name = "Crédito")] Credit
}

//Débito = Entrada de Valores, Crédito = Saída de Valores