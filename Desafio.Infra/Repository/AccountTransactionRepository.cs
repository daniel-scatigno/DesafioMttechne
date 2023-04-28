using Desafio.Infra.Models;
using Microsoft.EntityFrameworkCore;
namespace Desafio.Infra.Repository
{
   public class AccountTransactionRepository: Repository<AccountTransaction>
   {
      public AccountTransactionRepository(DbContext context):base(context)
      {

      }

   }
}