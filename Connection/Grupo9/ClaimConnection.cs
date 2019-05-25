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
        ///     Metodo para agregar un clalamo
        /// </summary>
        /// <param name="claim"></param>
        public void AddClaim(Claim claim)
        {
            Connect();
            StoredProcedure("addclaim(@cla_title,@cla_descr)");
			AddParameter("cla_title", claim._title);
			AddParameter("cla_descr", claim._description);

            ExecuteQuery();
        }

        /// <summary>
        ///     Metodo para obtener un clalamo segun su id
        /// </summary>
        /// <param name="numero"></param>
        /// que el logro no existe
        /// </exception>
        /// <returns></returns>
        public List<Claim> GetClaim(int numero)
        {
            var ClaimList = new List<Claim>();

            Connect();
            StoredProcedure("getclaim(@cla_id)");
			AddParameter("cla_id", numero);
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
        ///     Metodo para elimar un clalamo con su id
        /// </summary>
        /// <param name="clalamoId"></param>
        public void DeleteClaim(int claimId)
        {
            Connect();
            StoredProcedure("DeleteClaim(@cla_id)");
			AddParameter("cla_id", claimId);
            ExecuteQuery();
        }

        public void ModifyClaimStatus(int claimId, Claim claim)
        {
            Connect();
            StoredProcedure("modifyclaimstatus(@cla_id,@cla_status)");
			AddParameter("cla_id", claimId);
			AddParameter("cla_status", claim._status);
            ExecuteQuery();
        }

        public void ModifyClaimTitle(int claimId, Claim claim)
        {
            Connect();
            StoredProcedure("modifyclaimtitle(@cla_id,@cla_title, @cla_descr)");
			AddParameter("cla_id", claimId);
			AddParameter("cla_title", claim._title);
			AddParameter("cla_descr",claim._description);
            ExecuteQuery();
        }
    }
}