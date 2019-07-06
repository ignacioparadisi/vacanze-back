using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    /// <summary>  
    ///  Comando para buscar un restaurantes por su locacion
    /// </summary> 
    public class GetRestaurantsByCityCommand : CommandResult<List<RestaurantDto>>
    {
        private int _locationId;
        private List<RestaurantDto> _restaurantDtoList;

        public GetRestaurantsByCityCommand(int locationId)
        {
            _locationId = locationId;
        }
        
        public void Execute()
        {
            _restaurantDtoList = new List<RestaurantDto>();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDtoList = restaurantMapper.CreateDTOList(daoFactory.GetRestaurantDAO().GetRestaurantsByCity(_locationId));
        }
        
        public List<RestaurantDto> GetResult()
        {
            return _restaurantDtoList;
        }
    }
}