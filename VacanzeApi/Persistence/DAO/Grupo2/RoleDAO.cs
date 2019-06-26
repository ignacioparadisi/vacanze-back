using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public interface RoleDAO
    {
        List<Entity> GetRoles();
        List<Role> GetRolesForUser(int userId);
        
    }
}