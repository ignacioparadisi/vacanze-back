using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class GetEmployeesCommand : Command, CommandResult<List<User>>
    {

        private static List<User> Employees;

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO users = factory.GetUserDAO();
                Employees = users.GetEmployees();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public List<User> GetResult()
        {
            return Employees;
        }
    }
}
