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
    public class GetRolesForUserCommand : Command, CommandResult<List<Role>>
    {
       // private readonly ILogger _logger;
        private static List<Role> Roles;
        public User User { get; set; }

        public GetRolesForUserCommand(User user)
        {
            User = user;
        }

        public void Execute()
        {
            try
            {
               // _logger.LogInformation("Entrando a Execute() GetRolesForUserCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                RoleDAO roles = factory.GetRoleDAO();
                Roles = roles.GetRolesForUser(User.Id);
            }
            catch (Exception e)
            {
               // _logger.LogError("Exception",e);
                Console.WriteLine(e.ToString());
            }
        }

        public List<Role> GetResult()
        {
            return Roles;
        }
    }
}
