using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class GetEmployeesCommand : Command, CommandResult<List<User>>
    {
        private readonly ILogger _logger;
        private static List<User> Employees;

        public void Execute()
        {
            try
            {
                _logger.LogInformation("Entrando a Execute() GetEmployeesCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO users = factory.GetUserDAO();
                Employees = users.GetEmployees();
            }
            catch(Exception e)
            {
                _logger.LogError("Exception",e);
                Console.WriteLine(e.ToString());
            }
        }

        public List<User> GetResult()
        {
            return Employees;
        }
    }
}
