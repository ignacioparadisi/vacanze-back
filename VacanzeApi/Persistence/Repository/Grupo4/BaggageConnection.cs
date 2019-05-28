using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo4
{
    public class BaggageConnection : Connection
    {
        public BaggageConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Metodo para agregar un ticket
        /// </summary>
        /// <param name="Baggage"></param>
        public void AddBaggage(Baggage baggage)
        {
           
            
            try
            {  
                Connect();
                StoredProcedure("addBaggage(@bag_res_fli_fk,@bag_res_cru_fk,@bag_status,@bag_descr)");
                AddParameter("bag_res_fli_fk", Baggage.MaletaFkVuelo);
                AddParameter("bag_res_cru_fk", Baggage.MaletaFkCrucero);
                AddParameter("bag_status", Baggage.MaletaStatus);
                AddParameter("bag_descr", Baggage.Descripcion);
                
                ExecuteQuery();   
            }
            catch (NpgsqlException )
            {
                throw new DatabaseException("error al agregar");
            }
            catch (Exception e) 
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        

        public void DeleteBaggage(int baggageId)
        {
            try{           
                Connect();
                StoredProcedure("DeleteBaggage(@bag_id)");
                AddParameter("bag_id", baggageId);
                ExecuteQuery();
            }catch (NpgsqlException)
            {
                throw new DatabaseException("error al eliminar");
            }
            catch (Exception e)
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public void ModifyBaggageStatus(int baggageId, Baggage baggage)
        {
            try{               
                Connect();
                StoredProcedure("modifyBaggageStatus(@bag_id,@bag_status)");
                AddParameter("bag_id", baggageId);
                AddParameter("bag_status", Baggage.MaletaStatus);
                ExecuteQuery();
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al modificar");
            }
            catch (Exception e)
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public void ModifyBaggageDescripcion(int baggageId, Baggage baggage)
        {
            try{
                Connect();
                StoredProcedure("modifyclaimtitle(@bag_id, @bag_descr)");
                AddParameter("bag_id", baggageId);
                AddParameter("bag_descr", Baggage.MaletaStatus);
               
                ExecuteQuery();
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al modificar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }
    }
}