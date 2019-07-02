
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class GetRestaurantCommand: CommandResult<RestaurantDto>
    {
        private int _id;
        private RestaurantDto _restaurantDto;

        public GetRestaurantCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDto = restaurantMapper.CreateDTO(daoFactory.GetRestaurantDAO().GetRestaurant(_id));
        }
        public RestaurantDto GetResult()
        {
            return _restaurantDto;
        }
    }
}