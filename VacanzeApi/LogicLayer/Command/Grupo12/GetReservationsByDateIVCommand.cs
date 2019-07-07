using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;
using vacanze_back.VacanzeApi.LogicLayer.Command;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo12
{
    public class GetReservationsByDateIVCommand : Command, CommandResult<List<Entity>>
    {
        private int Departure;
        private int Arrival;
        private string DepartureDate;
        private string ArrivalDate;
        private int NumPas; 
        private List<Entity> ListFlight;

        public GetReservationsByDateIVCommand(int departure, int arrival, string departuredate, string arrivaldate, int numpas)
        {
            this.Departure = departure;
            this.Arrival = arrival;
            this.DepartureDate = departuredate;
            this.ArrivalDate = arrivaldate;
            this.NumPas = numpas;
        }

        public void Execute(){
            
            try
            {

                //Obtiene el DAO correspondiente por medio de las factories
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                ReservationFlightDAO ResFlightDao = factory.GetReservationFlightDAO();

                
                bool ida = ResFlightDao.FindLocation(this.Departure);
                bool idb = ResFlightDao.FindLocation(this.Arrival);

                if( !ida  || !idb  ){

                    throw new ValidationErrorException("Una de las locaciones no existe");

                }


                ListFlight=ResFlightDao.GetReservationFlightIV(this.Departure, this.Arrival, this.DepartureDate, this.ArrivalDate, this.NumPas);

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
            return ListFlight;
        }

    }
}