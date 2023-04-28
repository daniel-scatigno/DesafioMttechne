namespace Desafio.Infra.Models
{
   public class AccountTransaction
   {
      public int Account { get; set; }

      public decimal Amount { get; set; }

      public DateTime Date { get; set; }

      public OperationType Type { get; set; }

      public decimal HistoricBalance { get; set; }


   }

   public enum OperationType { Debit, Credit }
}