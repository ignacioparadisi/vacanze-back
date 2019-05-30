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
        ///     Metodo para obtener un clalamo segun su id
        /// </summary>
        /// <param name="numero"></param>
        /// que el logro no existe
        /// </exception>
        /// <returns></returns>
        public List<Baggage> GetBaggage(int numero)
        {
            var BaggageList = new List<Baggage>();
            var table = new DataTable();
            if(numero == 0)
                table =PgConnection.Instance.ExecuteFunction("Baggage");
            else{   
                table = PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)",numero);
            }
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

        public List<Baggage> GetBaggageDocumentPasaport(int numero)
        {
            var BaggageList = new List<Baggage>();
            var table = PgConnection.Instance.ExecuteFunction("GetBaggageDocumentPasaport(@bag_id)",numero);
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

        public List<Baggage> GetBaggageDocumentCedula(int numero)
        {
            var BaggageList = new List<Baggage>();
            var table = PgConnection.Instance.ExecuteFunction("GetBaggageDocumentCedula(@bag_id)",numero);
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

		public List<Baggage> GetBaggageStatus(string bag_status)
        {
            var BaggageList = new List<Baggage>();
            var table = new DataTable();
            
            table = PgConnection.Instance.ExecuteFunction("getBaggagestatus(@bag_status)",bag_status);
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
 
        public int ModifyBaggageStatus(int BaggageId, Baggage Baggage)
        {               
            var table= PgConnection.Instance.ExecuteFunction("GetBaggage(@BAG_ID)",BaggageId);    
            if(table.Rows.Count < 1) throw new NullBaggageException("no existe esa id");                           
            PgConnection.Instance.ExecuteFunction("modifyBaggagestatus(@bag_id,@bag_status)",BaggageId ,Baggage._status);
            return BaggageId;
        }
    }
}