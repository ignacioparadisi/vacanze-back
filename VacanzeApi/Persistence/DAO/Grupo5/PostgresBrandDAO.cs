using System;
using System.Data;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5{

    public class PostgresBrandDAO : IBrandDAO {

        public int AddBrand (Brand brand) {
            int id = 0;
            try {
                PgConnection pgConnection = PgConnection.Instance;
                DataTable dataTable = pgConnection.ExecuteFunction (
                    "AddBrand(@brandName)", brand.BrandName);
                id = Convert.ToInt32 (dataTable.Rows[0][0]);
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
            return true;
        }
    }
}