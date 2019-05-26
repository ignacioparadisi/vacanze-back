using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo2
{
    public class UserConnection : Connection
    {
        public UserConnection()
        {
            CreateStringConnection();
        }
        
        /// <summary>
        /// Método para obtener todos los empleados de la BD
        /// </summary>
        /// <returns>Lista de usuarios que son empleados</returns>
        public List<User> GetEmployees()
        {
            var users = new List<User>();
            try
            {
                Connect();
                StoredProcedure("getEmployees()");
                ExecuteReader();
                for (int i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt64(GetString(i, 0));
                    var documentId = Convert.ToInt64(GetString(i, 1));
                    var name = GetString(i, 2);
                    var lastname = GetString(i, 3);
                    var email = GetString(i, 4);
                    var user = new User(id, documentId, name, lastname, email);
                    users.Add(user);
                }
            }
            catch (NpgsqlException e)
            {
                throw new DatabaseException("Error al buscar empleados");
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }

            return users;
        }
    }
}