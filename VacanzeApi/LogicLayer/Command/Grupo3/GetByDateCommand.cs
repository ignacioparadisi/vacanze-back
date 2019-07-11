using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3
{

    
    public class GetByDateCommand : Command, CommandResult<List<Entity>>{
        string departure,arrival;
        public GetByDateCommand(string depar,string arriv){
             this.departure=depar;
            this.arrival=arriv;
        }

        private List<Entity> entList;
        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IFlightDAO flightDao = factory.GetFlight();
            entList=flightDao.GetByDate(departure,arrival);
            //throw new System.NotImplementedException();
        }

        
        public List<Entity> GetResult(){

        
            return entList;
        }
    }

}