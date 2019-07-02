
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;


namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class UpdateRestaurantCommand : CommandResult<RestaurantDto>
    {
        private RestaurantDto _restaurantDto;
        public UpdateRestaurantCommand(RestaurantDto restaurantDto)
        {
            _restaurantDto = restaurantDto;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            Restaurant restaurant = restaurantMapper.CreateEntity(_restaurantDto);
            CommandFactory.CreateGetRestaurantValidatorCommand(restaurant).Execute();
            _restaurantDto = restaurantMapper.CreateDTO(daoFactory.GetRestaurantDAO().UpdateRestaurant(restaurant));
        }
        public RestaurantDto GetResult()
        {
            return _restaurantDto;
        }
    }
}