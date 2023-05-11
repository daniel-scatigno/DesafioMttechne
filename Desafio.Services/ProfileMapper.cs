using AutoMapper;

namespace Desafio.Services
{
   public class ProfileMapper : Profile
   {
      public ProfileMapper()
      {
         CreateMap<AccountTransaction, AccountTransactionViewModel>();
         CreateMap<AccountTransactionViewModel, AccountTransaction>();
      }
   }
}