global using Desafio.Infra.Models;
global using Desafio.Infra;
global using Desafio.Infra.Repository;
global using Desafio.Service.Mapping;
global using Desafio.ViewModels;
using AutoMapper;


namespace Desafio.Service;

public abstract class DesafioService<T> where T : class
{
   protected IRepository<T> Repo { get; set; }
   protected IMapper LibraryMapper { get; set; }
   protected IUnitOfWork UOW{get;set;}

   public DesafioService(IRepository<T> repo,IUnitOfWork uow)
   {
      Repo = repo;

      LibraryMapper = new MapperConfiguration(cfg =>
      {
         cfg.AddProfile(new ProfileMapper());  //mapping between Web and Business layer objects            
      }).CreateMapper();

      UOW = uow;

   }


}
