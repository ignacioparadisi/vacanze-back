using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public class PostgresRoleDAO: RoleDAO 
    {
        private const string SP_GETROLES = "GetRoles()";
        private const string SP_GETROLESFORUSER = "GetRolesForUser(@userId)";
        
        /// <summary>
        /// Método que Consulta Todos los Roles Existentes en Base de Datos.
        /// </summary>
        /// <returns>Una lista de Roles.</returns>
        public List<Role> GetRoles()
        {
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction(SP_GETROLES);
            
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }

            return roles;
        }

        /// <summary>
        /// Método que Consulta los Roles Corresponden a un Determinado Usuario.
        /// </summary>
        /// <param name="userId">Id del Usuario a Consultar.</param>
        /// <returns>Una lista de Roles.</returns>
        public List<Role> GetRolesForUser(int userId)
        {
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction(SP_GETROLESFORUSER, userId);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }

            return roles;
        }
    }
}