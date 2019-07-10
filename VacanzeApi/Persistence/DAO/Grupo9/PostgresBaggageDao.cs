using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo9
{
    public class PostgresBaggageDao : IBaggageDao
    {

        /// <summary>
        ///     Metodo para obtener un equipaje por su numero serial
        /// </summary>
        /// <param name="id">serial del equipaje</param>
        /// <returns>Baggage entity</returns>
        /// <exception cref="BaggageNotFoundException">No se encuentra un baggage con el serial especificado</exception>
        public Baggage GetById(int id)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)", id);

            if (resultTable.Rows.Count == 0)
                throw new BaggageNotFoundException("No existe la maleta con id " + id);

            return ExtractBaggageFromRow(resultTable.Rows[0]);
        }


        /// <summary>
        ///     Obtener todos los equipajes de un usuario en particular por medio de su documento de pasaporte
        /// </summary>
        /// <param name="passportId">el numero de pasaporte</param>
        /// <returns>Lista de objetos json de tipo equipaje</returns>
        public List<Baggage> GetByPassport(string passportId)
        {
            var baggagesByPassport = new List<Baggage>();
            var resultTable = PgConnection.Instance.ExecuteFunction("getBaggageDOcumentpasaport(@passportId)", passportId);
            for (var i = 0; i < resultTable.Rows.Count; i++)
            {
                var baggage = ExtractBaggageFromRow(resultTable.Rows[i]);
                baggagesByPassport.Add(baggage);
            }

            return baggagesByPassport;
        }



        /// <summary>
        ///     Metodo para obtener todos los equipajes segun un estatus determinado
        /// </summary>
        /// <param name="status">El nombre del estatus</param>
        /// <returns>Lista de objetos json de tipo equipaje</returns>
        public List<Baggage> GetByStatus(string status)
        {
            var baggagesBySatus = new List<Baggage>();
            var resultTable = PgConnection.Instance.ExecuteFunction("getBaggagestatus(@bag_status)", status);
            for (var i = 0; i < resultTable.Rows.Count; i++)
            {
                var baggage = ExtractBaggageFromRow(resultTable.Rows[i]);
                baggagesBySatus.Add(baggage);
            }

            return baggagesBySatus;
        }




        /// <summary>
        ///     Metodo para modificar el equipaje
        /// </summary>
        /// <param name="id">El id del equipaje que se quiere modificar</param>
        /// <param name="updatedBaggage">El nombre del equipaje</param>
        /// <returns>id del equipajes modificado</returns>
        public Baggage Update(int id, Baggage updatedBaggage)
        {
            //TODO: Hacer validator del baggage
            var table = PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)", id);
            if (table.Rows.Count == 0) throw new BaggageNotFoundException("No existe el elemento que desea modificar");
            PgConnection.Instance.ExecuteFunction("modifyBaggagestatus(@bag_id,@bag_status)", id,
                updatedBaggage.Status);
            return GetById(id);
        }



        private Baggage ExtractBaggageFromRow(DataRow row)
        {
            var id = Convert.ToInt32(row[0]);
            var descripcion = row[1].ToString();
            var status = row[2].ToString();
            var baggage = new Baggage(id, descripcion, status);

            return baggage;
        }

    }
}