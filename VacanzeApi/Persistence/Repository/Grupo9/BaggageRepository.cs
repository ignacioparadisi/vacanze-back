using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo9
{
    public class BaggageRepository 
    {
        /// <summary>
        /// Metodo para obtener un equipaje segun su id
        /// </summary>
        public List<Baggage> GetBaggage(int numero)
        {  
            var table = new DataTable();
            if(numero == 0)
                table =PgConnection.Instance.ExecuteFunction("Baggage");
            else   
                table = PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)",numero);
            return fillList(table);;
        }
        /// <summary>
        // Metodo para obtener equipaje segun un documento de identidad
        /// </summary>
        public List<Baggage> GetBaggageDocumentPasaport(string numero)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetBaggageDocumentPasaport(@bag_id)",numero);
            return fillList(table);;
        }
        /// <summary>
        // Metodo para obtener equipaje segun un estatus 
        /// </summary>
		public List<Baggage> GetBaggageStatus(string bag_status)
        {
            var table = PgConnection.Instance.ExecuteFunction("getBaggagestatus(@bag_status)",bag_status);
            return fillList(table);
        }
        /// <summary>
        // metodo para modificar el estatus de un equipaje
        /// </summary>
        public int ModifyBaggageStatus(int BaggageId, Baggage Baggage)
        {               
            var table= PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)",BaggageId);    
            if(table.Rows.Count < 1) throw new NullBaggageException("No existe el elemento que desea modificar");                           
            PgConnection.Instance.ExecuteFunction("modifyBaggagestatus(@bag_id,@bag_status)",BaggageId ,Baggage._status);
            return BaggageId;
        }
        /// <summary>
        // metodo que permite el llenado de las lista de los equipajes en los gets 
        /// </summary>
        private List<Baggage> fillList (DataTable table)
        {
            var BaggageList = new List<Baggage>(); 
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var descripcion = table.Rows[i][1].ToString();
                var status = table.Rows[i][2].ToString();
                var Baggage = new Baggage(id, descripcion, status);
                BaggageList.Add(Baggage);
            }
            return BaggageList;
        }
    }
}