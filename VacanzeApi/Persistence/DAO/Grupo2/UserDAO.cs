using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo2
{
    public interface UserDAO
    {
        List<User> GetEmployees();
        void VerifyEmail(string email, int id = 0);
        User GetUserById(int id);
        User AddUser(User entity);
        void AddUser_Role(int userid, int roleid);
        int DeleteUserById(int id);
        void DeleteUser_Role(int id);
        int UpdateUser(User entity, int id);
        
    }
}