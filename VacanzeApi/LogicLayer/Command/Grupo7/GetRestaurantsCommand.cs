using System.Collections.Generic;
using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class GetRestaurantsCommand: CommandResult<List<RestaurantDTO>>
    {
        private List<RestaurantDTO> _restaurantDtoList;
        public void Execute()
        {
            _restaurantDtoList = new List<RestaurantDTO>();
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresRestaurantDAO restaurantDao = (PostgresRestaurantDAO) daoFactory.GetRestaurantDAO();
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDtoList = restaurantMapper.CreateDTOList(restaurantDao.GetRestaurants());
        }
        public List<RestaurantDTO> GetResult()
        {
            return _restaurantDtoList;
        }
    }
}