using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo2
{
    public class RoleRepository
    {
        public static List<Role> GetRoles()
        {
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction("GetRoles()");
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt64(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }

            return roles;
        }

        public static List<Role> GetRolesForUser(long userId)
        {
            var roles = new List<Role>();
            var table = PgConnection.Instance.ExecuteFunction("GetRolesForUser(@userId)", userId);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var id = Convert.ToInt64(table.Rows[i][0]);
                var name = table.Rows[i][1].ToString();
                var role = new Role(id, name);
                roles.Add(role);
            }

            return roles;
        }
    }
    
}