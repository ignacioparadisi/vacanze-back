using DefaultNamespace;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    public class GetRestaurantCommand: CommandResult<RestaurantDTO>
    {
        private int _id;
        private RestaurantDTO _restaurantDto;

        public GetRestaurantCommand(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            PostgresRestaurantDAO restaurantDao = (PostgresRestaurantDAO) daoFactory.GetRestaurantDAO();
            RestaurantMapper restaurantMapper = MapperFactory.CreateRestaurantMapper();
            _restaurantDto = restaurantMapper.CreateDTO(restaurantDao.GetRestaurant(_id));
        }
        public RestaurantDTO GetResult()
        {
            return _restaurantDto;
        }
    }
}