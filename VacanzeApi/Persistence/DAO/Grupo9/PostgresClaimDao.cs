using System;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo9
{
    public class PostgresClaimDao : IClaimDao
    {
        public Claim GetById(int id)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)", id);

            if (resultTable.Rows.Count == 0)
                throw new ClaimNotFoundException("No existe el elemento con id " + id);

            return ExtractClaimFromRow(resultTable.Rows[0]);
        }

        public void Add(Claim claim)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Claim updatedFields)
        {
            throw new NotImplementedException();
        }

        private Claim ExtractClaimFromRow(DataRow row)
        {
            var id = Convert.ToInt32(row[0]);
            var title = row[1].ToString();
            var description = row[2].ToString();
            var status = row[3].ToString();
            var baggageId = Convert.ToInt32(row[4]);
            // TODO: Pila con esta condicion que tenian antes
//            Claim claim;
//            if (table.Columns.Count == 4)
//            {
//                claim = new Claim(id, titulo, descripcion, status);
//            }
//            else
//            {
//                var idEquipaje = Convert.ToInt32(table.Rows[i][4].ToString());
//                claim = new Claim(id, titulo, descripcion, status, idEquipaje);
//            }

            return new Claim(id, title, description, status, baggageId);
        }
    }
}