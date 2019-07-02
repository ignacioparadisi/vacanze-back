using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo7;

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
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            var restaurantMapper = MapperFactory.CreateRestaurantMapper();
            var restaurant = restaurantMapper.CreateEntity(_restaurantDto);
            CommandFactory.CreateGetRestaurantValidatorCommand(restaurant).Execute();
            _restaurantDto.Id = daoFactory.GetRestaurantDAO().AddRestaurant(restaurant);
        }
        public RestaurantDTO GetResult()
        {
            return _restaurantDto;
        }
    }
}