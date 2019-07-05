using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12
{
    public class GetIdReturnCityCommand : Command, CommandResult<List<int>>
    {
        private List<int> id_locations = new List<int>();
        private List<string> names_city = new List<string>();

        
        public GetIdReturnCityCommand(List<string> names_city) 
        {
            this.names_city = names_city;
        } 
        

        public void Execute(){

            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();
            id_locations.Add( ResFlightDao.GetIDLocation(names_city[0]) );
            id_locations.Add( ResFlightDao.GetIDLocation(names_city[1]) );

        }

        public List<int> GetResult(){
            return this.id_locations;
        }
    }
}