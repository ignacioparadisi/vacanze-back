using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5{

    public class PostgresBrandDAO : IBrandDAO {

        ///<sumary>Creación de una marca de carro</sumary>
        ///<param name="brand">Instancia de Brand</param>
        ///<returns>Id de marca</returns>
        ///<exception cref="UniqueAttributeException">
        /// Es excepción es lanzada cuando el nombre de la marca ya existe
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

        ///<sumary>Obtener todos las marcas</sumary>
        ///<returns>Lista de marcas</returns>
        ///<exception cref="WithoutExistenceOfBrandsException"> 
        /// Si no hay existencia de marcas se lanza esta exepción
        ///</exception>
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
                    throw new WithoutExistenceOfBrandsException("Sin existencia de marcas");
                }
             }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return brands;
        }

        ///<sumary>Obtener marcas por Id</sumary>
        ///<param name="brandId">Id de la marca</param>
        ///<returns>Instancia de la marca</returns>
        ///<exception cref="BrandNotFoundException"> Si la marca no existe </exception>
        public Brand GetBrandById(int brandId){
            Brand brand = null;
             try{
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction("GetBrandById(@brandId)", brandId);
                if(dataTable.Rows.Count == 0){
                    throw new BrandNotFoundException(brandId, "No se ha encontrado una Marca con Id ");
                }
                else{
                    brand = new Brand(
                        Convert.ToInt32(dataTable.Rows[0][0]),
                        dataTable.Rows[0][1].ToString()
                    );
                }
             }catch(DatabaseException ex){
                throw new InternalServerErrorException("Error en el servidor : Conexion a base de datos", ex);
            }catch(InvalidStoredProcedureSignatureException ex){
                throw new InternalServerErrorException("Error en el servidor", ex);
            }
            return brand;
        }

        ///<sumary>Modificar una marca</sumary>
        ///<param name="marca">Instancia de una marca</param>
        ///<returns>True si actualizo correctamente</returns>
        ///<exception cref="BrandNotFoundException"> Si la marca no existe </exception>
        ///<exception cref="UniqueAttributeException">
        ///Si ya existe una marca con el mismo nombre
        ///</exception>
        public bool UpdateBrand(Brand brand){
            bool updated = false;
            try{
                GetBrandById(brand.Id);//Throw BrandNotFoundException
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