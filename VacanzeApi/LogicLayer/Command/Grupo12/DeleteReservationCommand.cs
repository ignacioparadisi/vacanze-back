using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12
{
    public class DeleteReservationCommand : Command
    {
        private int Id;

        public DeleteReservationCommand(int Id)
        {
            this.Id = Id;
        }

        public void Execute(){

            try
            {

                //Obtiene el DAO correspondiente por medio de las factories
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();

                //Valida que exista la reserva
                bool f = ResFlightDao.FindReservation(Id); 
                if( !f ){

                        throw new ValidationErrorException("La reserva a borrar no existe");

                }

                ResFlightDao.DeleteReservationFlight(this.Id);

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
    }
}