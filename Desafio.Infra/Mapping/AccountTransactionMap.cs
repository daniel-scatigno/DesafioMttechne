using Microsoft.EntityFrameworkCore;
using Desafio.Infra.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infra.Mapping;
public class AccountTransactionMap : IEntityTypeConfiguration<AccountTransaction>
{
   public void Configure(EntityTypeBuilder<AccountTransaction> builder)
   {
      builder.HasKey(x=>x.Id);
      

   }
}
