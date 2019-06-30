using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public interface UserDAO
    {
        List<Entity> GetEmployees();
        void VerifyEmail(string email, int id = 0);
        Entity GetUserById(int id);
        Entity AddUser(Entity entity);
        void AddUser_Role(int userid, int roleid);
        int DeleteUserById(int id);
        void DeleteUser_Role(int id);
        int UpdateUser(Entity entity, int id);
        
    }
}