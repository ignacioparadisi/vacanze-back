using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;
namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3
{
    public class GetFindFlightIdCommand: Command, CommandResult<Entity>
    {

        private int _id;
         Entity i;
        public GetFindFlightIdCommand (int _id) {
            this._id = _id;
        }

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IFlightDAO flightDao = factory.GetFlight();
                i = flightDao.FindIdEntity(_id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Entity GetResult()
        {
            return i;
        }
    }
}
