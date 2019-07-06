using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5
{
    public class PostgresModelDAO : IModelDAO 
    {
        public int AddModel(Model model){
             int id = 0;
            try {
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "AddModel(@brandId, @modelName, @capacity, @picture)", 
                        model.ModelBrandId, model.ModelName, model.Capacity, model.Picture);
                    id = Convert.ToInt32 (dataTable.Rows[0][0]);
                
            } catch (DatabaseException ex) {
                throw new InternalServerErrorException ("Error en el servidor : Conexion a base de datos", ex);
            } catch (InvalidStoredProcedureSignatureException ex) {
                throw new InternalServerErrorException ("Error en el servidor", ex);
            }
            return id;
        }
    }
}