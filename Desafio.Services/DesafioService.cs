global using Desafio.Infra.Models;
global using Desafio.Infra;
global using Desafio.Infra.Repository;
global using Desafio.Services;
global using Desafio.ViewModels;
using AutoMapper;


namespace Desafio.Services;

public abstract class DesafioService<T> where T : class
{
   protected IRepository<T> Repo { get; set; }
   protected IMapper DesafioMapper { get; set; }
   protected IUnitOfWork UOW{get;set;}

   public DesafioService(IRepository<T> repo,IUnitOfWork uow)
   {
      Repo = repo;

      DesafioMapper = new MapperConfiguration(cfg =>
      {
         cfg.AddProfile(new ProfileMapper());  //mapping between Web and Business layer objects            
      }).CreateMapper();

      UOW = uow;

   }


}
