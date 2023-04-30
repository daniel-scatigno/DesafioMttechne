using Microsoft.AspNetCore.Mvc;
using Desafio.Services;

namespace Desafio.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DesafioController<ViewModel, Model> : ControllerBase
   where ViewModel : class, new()
   where Model : class, new()
{
   public DesafioService<Model> Service { get; }
   public DesafioController(DesafioService<Model> service)
   {
      Service = service;
   }

}
