using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5{

    public class PostgresBrandDAO : IBrandDAO {

        ///<sumary>
        /// Creación de una marca de carro
        ///</sumary>
        ///<param name="brand">Instancia de Brand, contiene todos los atributos necesarios
        ///   para realizar la creación de una marca</param>
        ///<returns>
        /// El id del Brand que fue creado
        ///</returns>
        ///<exception cref="UniqueAttributeException">
        /// Es excepción es lanzada cuando el nombre de la marca ya existe
        ///</exception>
        ///<exception cref="InternalServerErrorException">
        /// Es lanzada cuando ocurre un problema de conexión en la base de datos, o
        /// cuando un error en el servidor
        ///</exception>
        public int AddBrand (Brand brand) {
            int id = 0;
            try {
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "BrandUniqueness(@brandName)", brand.BrandName);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    throw new UniqueAttributeException("La marca " + brand.BrandName + " ya existe");
                }else{
                    dataTable = pgConnection.ExecuteFunction (
                    "AddBrand(@brandName)", brand.BrandName);
                    id = Convert.ToInt32 (dataTable.Rows[0][0]);
                }
            } catch (DatabaseException ex) {
                throw new InternalServerErrorException ("Error en el servidor : Conexion a base de datos", ex);
            } catch (InvalidStoredProcedureSignatureException ex) {
                throw new InternalServerErrorException ("Error en el servidor", ex);
            }
            return id;
        }

        public List<Brand> GetBrands(){
            List<Brand> brands = new List<Brand>();
             try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetBrands()");
                if( dataTable.Rows.Count > 0){
                    foreach (DataRow dataRow in dataTable.Rows){
                        Brand brand = new Brand(
                            Convert.ToInt32(dataRow[0]),
                            dataRow[1].ToString()
                        );
                        brands.Add(brand);
                    }
                }else{
                    // Throw Exception
                }
             }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return brands;
        }

        public bool UpdateBrand(Brand brand){
            bool updated = false;
            try{
                //Throw Exception If User Doesn't exist
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "BrandUniqueness(@brandName)", brand.BrandName);
                if(Convert.ToBoolean(dataTable.Rows[0][0])){
                    throw new UniqueAttributeException("La marca " + brand.BrandName + " ya existe");
                }else{
                    dataTable = pgConnection.ExecuteFunction("UpdateBrand(@vbid, @vbname)",brand.Id, brand.BrandName);
                    if(Convert.ToBoolean(dataTable.Rows[0][0])){
                        updated = true;
                    }
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