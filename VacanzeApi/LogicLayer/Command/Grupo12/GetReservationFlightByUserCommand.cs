using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

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

            try
            {

 
                 //Obtiene el DAO correspondiente por medio de las factories
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();

                 //Valida que el usuario existe
                bool f = ResFlightDao.FindUser(this.IdUser); 
                if( !f ){

                        throw new ValidationErrorException("El usuario no existe");

                }

                 this.FlightReservations = ResFlightDao.GetReservationFlight(this.IdUser);
            }
            catch (ValidationErrorException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Entity> GetResult(){
            return this.FlightReservations;
        }
    }
}