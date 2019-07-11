using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5
{
    public class PostgresModelDAO : IModelDAO 
    {
        ///<sumary>Creación de un modelo</sumary>
        ///<param name="model">Instancia de Model</param>
        ///<returns>Id del modelo</returns>
        ///<exception cref="UniqueAttributeException">
        /// Es excepción es lanzada cuando el nombre del modelo ya existe
        ///</exception>
        public int AddModel(Model model){
             int id = 0;
            try {
                PostgresBrandDAO brandDAO = new PostgresBrandDAO();
                brandDAO.GetBrandById(model.ModelBrandId);//Throw BrandNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "ModelUniqueness(@modelName)", model.ModelName);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    throw new UniqueAttributeException("El modelo " + model.ModelName + " ya existe");
                }else{
                    dataTable = pgConnection.ExecuteFunction (
                    "AddModel(@brandId, @modelName, @capacity, @picture)", 
                        model.ModelBrandId, model.ModelName, model.Capacity, model.Picture);
                    id = Convert.ToInt32 (dataTable.Rows[0][0]);
                }
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
                    throw new WithoutExistenceOfModelsException("Sin existencia de Modelos");
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return models;
        }

        public List<Model> GetModelsByBrand(int brandId){
            List<Model> models = new List<Model>();
            try{
                PostgresBrandDAO brandDAO = new PostgresBrandDAO();
                brandDAO.GetBrandById(brandId);//Throw BrandNotFoundException
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetModelsByBrand(@brandId)", brandId);
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
                    throw new WithoutExistenceOfModelsException("No se encontraron Modelos para dicha Marca");
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return models;
        }

        public Model GetModelById(int modelId){
            Model model = null;
            try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetModelById(@modelId)", modelId);
                if(dataTable.Rows.Count == 0){
                    throw new ModelNotFoundException(modelId, "No se ha encontrado un Modelo con Id ");
                }
                else{
                    model = new Model(
                        Convert.ToInt32(dataTable.Rows[0][0]),
                        Convert.ToInt32(dataTable.Rows[0][1]),
                        dataTable.Rows[0][2].ToString(),
                        Convert.ToInt32(dataTable.Rows[0][3]),
                        dataTable.Rows[0][4].ToString()
                    );
                    model.ModelBrand = new PostgresBrandDAO().GetBrandById(model.ModelBrandId);
                }
            }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return model;
        }

        public bool UpdateModel(Model model){
            bool updated = false;
            try{
                GetModelById(model.Id);//Throw ModelNotFoundException
                PostgresBrandDAO brandDAO = new PostgresBrandDAO();
                brandDAO.GetBrandById(model.ModelBrandId);//Throw BrandNotFoundException
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