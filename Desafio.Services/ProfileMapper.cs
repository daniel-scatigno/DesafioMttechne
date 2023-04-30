
using AutoMapper;
using Desafio.Infra.Models;
using Desafio.ViewModels;

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