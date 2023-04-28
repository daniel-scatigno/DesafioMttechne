
using AutoMapper;
using AutoMapper.Internal;
namespace Desafio.Service;

public static class MappingExtension
{
   public static object AutoMap(this IMapper mapper, object obj)
   {

      var sourceType = obj.GetType();
      var destinationType = mapper.ConfigurationProvider.Internal().GetAllTypeMaps()
        .Where(map => map.SourceType == sourceType).
        
        // No automap é assumido que só existe um mapeamento para sua classe
        Single().
        DestinationType;

      return mapper.Map(obj, sourceType, destinationType);

   }
}
