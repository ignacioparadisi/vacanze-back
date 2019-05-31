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
        /// Metodo para agregar un reclamo
        /// </summary>
        public void AddClaim(Claim claim, int id)
        {  
            var table = PgConnection.Instance.ExecuteFunction(
            "addclaim(@cla_title,@cla_descr, @bag_int)",
            claim._title, claim._description, id);
        }
        /// <summary>
        /// Metodo para obtener el numero de reclamos 
        /// </summary>
        public int GetClaim()
        {              
            var table = PgConnection.Instance.ExecuteFunction("claim");
            return table.Rows.Count;
        }
        /// <summary>
        ///  Metodo para obtener un reclamo segun su id
        /// </summary>
        public List<Claim> GetClaim(int numero)
        {
            var table = new DataTable();
            if(numero == 0)
                table = PgConnection.Instance.ExecuteFunction("claim");
            else 
                table = PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",numero);
            return fillList(table);
        }
        /// <summary>
        // Metodo para obtener los reclamos segun un tipo de estatus
        /// </summary>        
		public List<Claim> GetClaimStatus(string cla_status)
        {       
            var table = PgConnection.Instance.ExecuteFunction("getclaimstatus(@cla_status)",cla_status);       
            return fillList(table);;
        }
        /// <summary>
        // Metodo para obtener reclamo segun un documento de identidad
        /// </summary>
        public List<Claim> GetClaimDocument(string numero)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetClaimDocument(@cla_id)",numero);
            return fillList(table);;
        }
        /// <summary>
        // eliminar un reclamo
        /// </summary>
        public int  DeleteClaim(int claimId)
        {
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("No existe el elemento que desea eliminar");
            PgConnection.Instance.ExecuteFunction("deleteclaim(@cla_id)",claimId );
            var id = Convert.ToInt32(table.Rows[0][0].ToString());
            return id;
        }
        /// <summary>
        // modificar el estatus de un reclamo 
        /// </summary>
        public int ModifyClaimStatus(int claimId, Claim claim)
        {               
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("No existe el elemento que desea modificar");                           
            PgConnection.Instance.ExecuteFunction("modifyclaimstatus(@cla_id,@cla_status)",claimId ,claim._status);
            return claimId;
        }
        /// <summary>
        // modificar el titulo y descripcion  de un reclamo 
        /// </summary>
        public int ModifyClaimTitle(int claimId, Claim claim)
        {
            var table= PgConnection.Instance.ExecuteFunction("getclaim(@cla_id)",claimId);    
            if(table.Rows.Count < 1) throw new NullClaimException("nNo existe el elemento que desea modificar");                           
            PgConnection.Instance.ExecuteFunction("modifyclaimtitle(@cla_id,@cla_title,@cla_descr)",claimId ,claim._title,claim._description);
            return claimId;  
        }
        /// <summary>
        // metodo que permite el llenado de las lista de los reclamos en los gets 
        /// </summary>
        private List<Claim> fillList (DataTable table)
        {
            var ClaimList = new List<Claim>(); 
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
    }
}