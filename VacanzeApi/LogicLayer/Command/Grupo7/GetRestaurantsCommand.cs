using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    /// <summary>  
    ///  Comando para retornar todos los restaurantes existentes
    /// </summary> 
    public class GetRestaurantsCommand: CommandResult<List<RestaurantDto>>
    {
        private List<RestaurantDto> _restaurantDtoList;
        public void Execute()
        {
            _restaurantDtoList = new List<RestaurantDto>();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDtoList = restaurantMapper.CreateDTOList(daoFactory.GetRestaurantDAO().GetRestaurants());
        }
        public List<RestaurantDto> GetResult()
        {
            return _restaurantDtoList;
        }
    }
}