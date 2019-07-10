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
    public class PostUser_RoleCommand : Command
    {
        private readonly ILogger _logger;
        public Role Role { get; set; }
        public int Id { get; set; }

        public PostUser_RoleCommand(int id, Role role)
        {
            this.Id = id;
            this.Role = role;
        }

        public void Execute()
        {
            try
            {
                _logger.LogInformation("Entrando a Execute() PostUser_RoleCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.AddUser_Role(this.Id,this.Role.Id);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception",e);
                Console.WriteLine(e.ToString());
            }
        }
    }
}
