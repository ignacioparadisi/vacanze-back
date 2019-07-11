


using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3
{

    
    public class GetFlightListCommand : Command, CommandResult<List<Entity>>{
        public GetFlightListCommand(){}

        private List<Entity> entList;
        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IFlightDAO flightDao = factory.GetFlight();
            entList=flightDao.Get();
            //throw new System.NotImplementedException();
        }

        
        public List<Entity> GetResult(){

        
            return entList;
        }
    }

}