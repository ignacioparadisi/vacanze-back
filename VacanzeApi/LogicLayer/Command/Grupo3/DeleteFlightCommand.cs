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
    public class DeleteFlightCommand: Command
    {

        private int _id;
     
        public DeleteFlightCommand (int _id) {
            this._id = _id;
        }

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IFlightDAO flightDao = factory.GetFlight();
                flightDao.Delete(_id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
