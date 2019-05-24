using System;
using System.Collections.Generic;
using vacanze_back.Entities.Grupo9;

namespace vacanze_back.Connection.Grupo9
{
    public class ClaimConnection : Connection
    {
        public ClaimConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Metodo para agregar un reclamo
        /// </summary>
        /// <param name="claim"></param>
        public void AddClaim(Claim claim)
        {
            Connect();
            StoredProcedure("AgregarReclamo(@rec_titulo,@rec_descripcion,@rec_status)");
			AddParameter("rec_titulo", claim._title);
			AddParameter("rec_descripcion", claim._description);
			AddParameter("rec_status", claim._status);

            ExecuteQuery();
        }

        /// <summary>
        ///     Metodo para obtener un reclamo segun su id
        /// </summary>
        /// <param name="numero"></param>
        /// que el logro no existe
        /// </exception>
        /// <returns></returns>
        public List<Claim> GetClaim(int numero)
        {
            var ClaimList = new List<Claim>();

            Connect();
            StoredProcedure("ConsultarUnReclamo(@idReclamo)");
			AddParameter("idReclamo", numero);
            ExecuteReader();

            for (var i = 0; i < cantidadRegistros; i++)
            {
                var id = Convert.ToInt32(GetString(i, 0));
                var titulo = GetString(i, 1);
                var descripcion = GetString(i, 2);
                var status = GetString(i, 3);
                var claim = new Claim(id, titulo, descripcion, status);
                ClaimList.Add(claim);
            }

            return ClaimList;
        }

        /// <summary>
        ///     Metodo para elimar un reclamo con su id
        /// </summary>
        /// <param name="reclamoId"></param>
        public void DeleteClaim(int claimId)
        {
            Connect();
            StoredProcedure("EliminarReclamo(@rec_id)");
			AddParameter("rec_id", claimId);
            ExecuteQuery();
        }

        public void ModifyClaimStatus(long claimId, Claim claim)
        {
            Connect();
            StoredProcedure("ModificarReclamoStatus(@rec_id,@rec_status)");
			AddParameter("rec_id", claimId);
			AddParameter("rec_status", claim._status);
            ExecuteQuery();
        }

        public void ModifyClaimTitle(long claimId, Claim claim)
        {
            Connect();
            StoredProcedure("ModificarReclamoTitulo(@rec_id,@rec_titulo)");
			AddParameter("rec_id", claimId);
			AddParameter("rec_titulo", claim._title);
            ExecuteQuery();
        }
    }
}