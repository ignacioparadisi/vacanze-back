using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo4;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo4
{
    public class PostgresSaleFlightDAO
    {
        private const string SP_SELECTFLIGHT = "GetAvailableFlight(@_origin, @_destination,@_dateArrival, @_dateDeparture)";

        public List<SaleFlight> GetAvailableFlights(int origin, int destination, DateTime dateArrival,DateTime dateDeparute)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction(SP_SELECTFLIGHT,origin,destination,dateArrival,dateDeparute);
            if (resultTable.Rows.Count == 0)
                throw new FlightNotFoundException();

            return RowToSaleFlightList(resultTable);
        }

        public List<SaleFlight> RowToSaleFlightList(DataTable dt)
        {
            var _oResp = new List<SaleFlight>();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var hotel = RowToSaleFlight(dt.Rows[i]);
                _oResp.Add(hotel);
            }

            return _oResp;
        }

        public SaleFlight RowToSaleFlight(DataRow row)
        {

            try
            {
                var _oResp = new SaleFlight();

                _oResp.id = Convert.ToInt32(row[1]);
                _oResp.ileft = Convert.ToInt32(row[2]);
                _oResp.descrip = Convert.ToString(row[3]);
                _oResp.dateArrival = Convert.ToDateTime(row[4]);
                _oResp.price = Convert.ToDecimal(row[5]);
                _oResp.origin = Convert.ToString(row[6]);
                _oResp.destination = Convert.ToString(row[7]);

                return _oResp;
            }
            catch (Exception)
            {

                throw new NotValidFactoryTypeException("Error en los campos de salida");
            }
           
        }
    }
}
