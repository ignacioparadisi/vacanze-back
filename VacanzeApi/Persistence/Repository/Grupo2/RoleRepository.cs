using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
    /// <summary> 
    /// Clase que Realiza las Operaciones Contra la Tabla Roles.
    /// </summary>
    public class RoleRepository
    {
        /// <summary>
        /// Método que Consulta Todos los Roles Existentes en Base de Datos.
        /// </summary>
        /// <returns>Una lista de Roles.</returns>
        public static List<Entity> GetRoles()
        {
            var roles = new List<Entity>();
            var table = PgConnection.Instance.ExecuteFunction("GetRoles()");
            
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
        public static List<Role> GetRolesForUser(int userId)
        {
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction("GetRolesForUser(@userId)", userId);
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