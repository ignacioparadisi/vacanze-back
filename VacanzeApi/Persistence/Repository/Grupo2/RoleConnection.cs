using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
    public class RoleConnection : Connection
    {
        public RoleConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Metodo para consultar todos los roles
        /// </summary>
        public List<Role> GetRoles()
        {
            var roles = new List<Role>();
            try
            {
                Connect();
                StoredProcedure("GetRoles()");
                ExecuteReader();
                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var name = GetString(i, 1);
                    var role = new Role(id, name);
                    roles.Add(role);
                }

                return roles;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException("error al buscar roles");
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }
        }

        public List<Role> GetRolesForUser(long userId)
        {
            var roles = new List<Role>();
            try
            {
                Connect();
                StoredProcedure("GetRolesForUser(" + userId + ")");
                ExecuteReader();
                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt64(GetString(i, 0));
                    var name = GetString(i, 1);
                    var role = new Role(id, name);
                    roles.Add(role);
                }

                return roles;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException("Error al buscar roles para el usuario " + userId);
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }
        }
    }
}