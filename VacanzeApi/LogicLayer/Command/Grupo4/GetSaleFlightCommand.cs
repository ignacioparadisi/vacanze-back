using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo4
{
    public class GetSaleFlightCommand: Command, CommandResult<List<SaleFlight>>
    {
        private List<SaleFlight> _saleFlights;
        private int _origin;
        private int _destination;
        private DateTime _dateArrival;
        private DateTime _dateDeparute;

        public GetSaleFlightCommand(int origin, int destination, DateTime dateArrival, DateTime dateDeparute)
        {
            _origin = origin;
            _destination = destination;
            _dateArrival = dateArrival;
            _dateDeparute = dateDeparute;

        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);

            _saleFlights = daoFactory.GetSaleFlightDAO().GetSaleFlight(_origin,_destination,_dateArrival,_dateDeparute);
        }

        public List<SaleFlight> GetResult()
        {
            return _saleFlights;
        }


    }
}
