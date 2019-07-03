using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class AddRestaurantCommand
    {
        private RestaurantDTO _restaurantDto;
        
        public AddRestaurantCommand(RestaurantDTO restaurantDto)
        {
            _restaurantDto = restaurantDto;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresRestaurantDAO restaurantDao = (PostgresRestaurantDAO) daoFactory.GetRestaurantDAO();
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            Restaurant restaurant = (Restaurant) restaurantMapper.CreateEntity(_restaurantDto);
            _restaurantDto.Id = restaurantDao.AddRestaurant(restaurant);
        }
        public RestaurantDTO GetResult()
        {
            return _restaurantDto;
        }
    }
}