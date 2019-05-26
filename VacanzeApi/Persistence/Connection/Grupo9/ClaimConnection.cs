using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo9
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
            try{   
                Connect();
                StoredProcedure("addclaim(@cla_title,@cla_descr)");
                AddParameter("cla_title", claim._title);
                AddParameter("cla_descr", claim._description);
                ExecuteQuery();   
            }
            catch (NpgsqlException )
            {
                throw new DatabaseException("error al agregar");
            }
            catch (Exception e) 
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }
       /// <summary>
        ///     Metodo para obtener los numero de reclamos en la tabla reclamo
        /// </summary>
        public int GetClaim()
        {
            try{            
                Connect();
                StoredProcedure("claim");
                ExecuteReader();
                return numberRecords;
            }catch (NpgsqlException)
            {
                throw new DatabaseException("error al consultar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
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
            try{            
                var ClaimList = new List<Claim>();

                Connect();
                if(numero == 0)
                    StoredProcedure("claim");
                else{
                    StoredProcedure("getclaim(@cla_id)");
                    AddParameter("cla_id", numero);
                }
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var titulo = GetString(i, 1);
                    var descripcion = GetString(i, 2);
                    var status = GetString(i, 3);
                    var claim = new Claim(id, titulo, descripcion, status);
                    ClaimList.Add(claim);
                }

                return ClaimList;
            }catch (NpgsqlException)
            {
                throw new DatabaseException("error al consultar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public List<Claim> GetClaimBaggage(int numero)
        {
            try{
                
                var ClaimList = new List<Claim>();
                Connect();
                StoredProcedure("GetClaimBaggage(@cla_id)");
                AddParameter("cla_id", numero);
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var titulo = GetString(i, 1);
                    var descripcion = GetString(i, 2);
                    var status = GetString(i, 3);
                    var claim = new Claim(id, titulo, descripcion, status);
                    ClaimList.Add(claim);
                }

                return ClaimList;
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al consultar");
            }
            catch (Exception e)
            {
                throw new GeneralException( e,DateTime.Now);
            }

            
        }

        public List<Claim> GetClaimDocumentPasaport(int numero)
        {
            try{
                var ClaimList = new List<Claim>();

                Connect();
                StoredProcedure("GetClaimDocumentPasaport(@cla_id)");
                AddParameter("cla_id", numero);
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var titulo = GetString(i, 1);
                    var descripcion = GetString(i, 2);
                    var status = GetString(i, 3);
                    var claim = new Claim(id, titulo, descripcion, status);
                    ClaimList.Add(claim);
                }

                return ClaimList;
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al consultar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public List<Claim> GetClaimDocumentCedula(int numero)
        {
            
            try{
                var ClaimList = new List<Claim>();

                Connect();
                StoredProcedure("GetClaimDocumentCedula(@cla_id)");
                AddParameter("cla_id", numero);
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var titulo = GetString(i, 1);
                    var descripcion = GetString(i, 2);
                    var status = GetString(i, 3);
                    var claim = new Claim(id, titulo, descripcion, status);
                    ClaimList.Add(claim);
                }
                return ClaimList;
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al consultar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public int DeleteClaim(int claimId)
        {
            try{          
                GetClaim(claimId); 
                if(numberRecords < 1) throw new NullClaimException("no existe esa id");
                Connect();
                StoredProcedure("DeleteClaim(@cla_id)");
                AddParameter("cla_id", claimId);
                ExecuteQuery();
                return numberRecords;
            }catch (NpgsqlException)
            {
                throw new DatabaseException("error al eliminar");
            }
            catch (Exception e)
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public int ModifyClaimStatus(int claimId, Claim claim)
        {
            try{ 
                
                GetClaim(claimId); 
                if(numberRecords < 1) throw new NullClaimException("no existe esa id");              
                Connect();
                StoredProcedure("modifyclaimstatus(@cla_id,@cla_status)");
                AddParameter("cla_id", claimId);
                AddParameter("cla_status", claim._status);              
                ExecuteQuery();
                return numberRecords;
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al modificar");
            }
            catch (Exception e)
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }

        public int ModifyClaimTitle(int claimId, Claim claim)
        {
            try{
                
                GetClaim(claimId); 
                if(numberRecords < 1) throw new NullClaimException("no existe esa id");
                Connect();
                StoredProcedure("modifyclaimtitle(@cla_id,@cla_title, @cla_descr)");
                AddParameter("cla_id", claimId);
                AddParameter("cla_title", claim._title);
                AddParameter("cla_descr",claim._description);
                ExecuteQuery();
                return numberRecords;
            }catch (NpgsqlException )
            {
                throw new DatabaseException("error al modificar");
            }
            catch (Exception e )
            {
                throw new GeneralException( e,DateTime.Now);
            }
        }
    }
}