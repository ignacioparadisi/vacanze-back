using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class UpdateRestaurantCommand : CommandResult<RestaurantDTO>
    {
        private RestaurantDTO _restaurantDto;
        public UpdateRestaurantCommand(RestaurantDTO restaurantDto)
        {
            _restaurantDto = restaurantDto;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresRestaurantDAO restaurantDao = (PostgresRestaurantDAO) daoFactory.GetRestaurantDAO();
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            Restaurant restaurant = restaurantMapper.CreateEntity(_restaurantDto);
            var id = _restaurantDto.Id;
            _restaurantDto = restaurantMapper.CreateDTO(restaurantDao.UpdateRestaurant(restaurant));
            _restaurantDto.Id = id;
        }
        public RestaurantDTO GetResult()
        {
            return _restaurantDto;
        }
    }
}