using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public class PostgresRoleDAO: RoleDAO 
    {
        private const string SP_GETROLES = "GetRoles()";
        private const string SP_GETROLESFORUSER = "GetRolesForUser(@userId)";
       // private readonly ILogger _logger;

        /// <summary>
        /// Método que Consulta Todos los Roles Existentes en Base de Datos.
        /// </summary>
        /// <returns>Una lista de Roles.</returns>
        public List<Role> GetRoles()
        {
           // _logger.LogInformation("Entrando a GetRoles()");
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction(SP_GETROLES);
            
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }
           // _logger.LogDebug("Roles",roles);
           // _logger.LogInformation("Saliendo de GetRoles()");
            return roles;
        }

        /// <summary>
        /// Método que Consulta los Roles Corresponden a un Determinado Usuario.
        /// </summary>
        /// <param name="userId">Id del Usuario a Consultar.</param>
        /// <returns>Una lista de Roles.</returns>
        public List<Role> GetRolesForUser(int userId)
        {
           // _logger.LogInformation("Entrando a GetRolesForUser(int userId)",userId);
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction(SP_GETROLESFORUSER, userId);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt32(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }
           // _logger.LogDebug("Roles", roles);
           // _logger.LogInformation("Saliendo de GetRolesForUser(int userId)", userId);
            return roles;
        }
    }
}