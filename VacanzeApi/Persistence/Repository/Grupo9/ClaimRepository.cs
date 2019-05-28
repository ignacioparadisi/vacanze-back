using System;
using System.Data;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo9
{
    public class ClaimRepository 
    {
        /// <summary>
        ///     Metodo para agregar un clalamo
        /// </summary>
        /// <param name="claim"></param>
        public void AddClaim(Claim claim)
        {  
            var table = PgConnection.Instance.ExecuteFunction(
            "addclaim(@cla_title,@cla_descr)",
            claim._title, claim._description);
        }
       /// <summary>
        ///     Metodo para obtener los numero de reclamos en la tabla reclamo
        /// </summary>
        public int GetClaim()
        {              
            var table = PgConnection.Instance.ExecuteFunction("claim");
            return table.Rows.Count;
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
            var table = new DataTable();
            if(numero == 0)
                table =PgConnection.Instance.ExecuteFunction("claim");
            else{   
                table = PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",numero);
            }
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var titulo = table.Rows[i][1].ToString();
                var descripcion = table.Rows[i][2].ToString();
                var status = table.Rows[i][3].ToString();
                var claim = new Claim(id, titulo, descripcion, status);
               ClaimList.Add(claim);
            }
            return ClaimList;
        }

        public List<Claim> GetClaimBaggage(int numero)
        {
            var ClaimList = new List<Claim>();
            var table = PgConnection.Instance.ExecuteFunction("GetClaimBaggage(@cla_id)",numero);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var titulo = table.Rows[i][1].ToString();
                var descripcion = table.Rows[i][2].ToString();
                var status = table.Rows[i][3].ToString();
                var claim = new Claim(id, titulo, descripcion, status);
                ClaimList.Add(claim);
            };
            return ClaimList;
        }

        public List<Claim> GetClaimDocumentPasaport(int numero)
        {
            var ClaimList = new List<Claim>();
            var table = PgConnection.Instance.ExecuteFunction("GetClaimDocumentPasaport(@cla_id)",numero);

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var titulo = table.Rows[i][1].ToString();
                var descripcion = table.Rows[i][2].ToString();
                var status = table.Rows[i][3].ToString();
                var claim = new Claim(id, titulo, descripcion, status);
                ClaimList.Add(claim);
            }
            return ClaimList;
        }

        public List<Claim> GetClaimDocumentCedula(int numero)
        {
            var ClaimList = new List<Claim>();
            var table = PgConnection.Instance.ExecuteFunction("GetClaimDocumentCedula(@cla_id)",numero);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0].ToString());
                var titulo = table.Rows[i][1].ToString();
                var descripcion = table.Rows[i][2].ToString();
                var status = table.Rows[i][3].ToString();
                var claim = new Claim(id, titulo, descripcion, status);
                ClaimList.Add(claim);
            };
            return ClaimList;
        }

        public int DeleteClaim(int claimId)
        {
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("no existe esa id");
            PgConnection.Instance.ExecuteFunction("DeleteClaim(@cla_id)",claimId );
            return claimId;
        }

        public int ModifyClaimStatus(int claimId, Claim claim)
        {               
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("no existe esa id");                           
            PgConnection.Instance.ExecuteFunction("modifyclaimstatus(@cla_id,@cla_status)",claimId ,claim._status);
            return claimId;
        }

        public int ModifyClaimTitle(int claimId, Claim claim)
        {
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("no existe esa id");                           
            PgConnection.Instance.ExecuteFunction("modifyclaimtitle(@cla_id,@cla_descr)",claimId ,claim._description);
            return claimId;  
        }
    }
}