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
    public class GetReservationFlightByUserCommand : Command, CommandResult<List<Entity>>
    {
        private int IdUser;
        private List<Entity> FlightReservations;

        
        public GetReservationFlightByUserCommand(int idUser) 
        {
            this.IdUser = idUser;
        } 
        

        public void Execute(){

            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();
            this.FlightReservations = ResFlightDao.GetReservationFlight(this.IdUser);;

        }

        public List<Entity> GetResult(){
            return this.FlightReservations;
        }
    }
}