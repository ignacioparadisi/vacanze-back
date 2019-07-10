using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class DeleteUser_RoleCommand : Command
    {
       // private readonly ILogger _logger;
        public int Id { get; set; }
        public DeleteUser_RoleCommand(int id)
        {
            this.Id = id;
        }
        public void Execute()
        {
            try
            {
               // _logger.LogInformation("Entrando a Execute() DeleteUser_RoleCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.DeleteUser_Role(Id);
            }
            catch (Exception e)
            {
               // _logger.LogError("Exception",e);
                Console.WriteLine(e.ToString());
            }
        }
    }
}
