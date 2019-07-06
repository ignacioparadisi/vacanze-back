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

        public List<Model> GetModels(){
            List<Model> models = new List<Model>();
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetModels()");
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Model model = new Model(
                            Convert.ToInt32(dataRow[0]),
                            Convert.ToInt32(dataRow[1]),
                            dataRow[2].ToString(),
                            Convert.ToInt32(dataRow[3]),
                            dataRow[4].ToString()
                        );
                    models.Add(model);
                    }
                }else{
                    // Throw Exception
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return models;
        }

        public bool UpdateModel(Model model){
            bool updated = false;
            try{
                //Throw Exception 
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction(
                    "UpdateModel(@vmid, @vmbrand, @vmname, @vmcapacity, @vmpicture)",
                    model.Id, model.ModelBrandId, model.ModelName, model.Capacity, model.Picture);
                    if(Convert.ToBoolean(dataTable.Rows[0][0])){
                        updated = true;
                    
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            
            return updated;
        }
    }
}