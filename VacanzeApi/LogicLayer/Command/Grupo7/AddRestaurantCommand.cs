
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class AddRestaurantCommand
    {
        private RestaurantDto _restaurantDto;
        
        public AddRestaurantCommand(RestaurantDto restaurantDto)
        {
            _restaurantDto = restaurantDto;
        }
        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var restaurantMapper = MapperFactory.CreateRestaurantMapper();
            var restaurant = restaurantMapper.CreateEntity(_restaurantDto);
            CommandFactory.CreateGetRestaurantValidatorCommand(restaurant).Execute();
            _restaurantDto.Id = daoFactory.GetRestaurantDAO().AddRestaurant(restaurant);
        }
        public RestaurantDto GetResult()
        {
            return _restaurantDto;
        }
    }
}