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
    public class PostUserCommand : Command, CommandResult<Entity>
    {
        //private readonly ILogger _logger;
        public User User { get; set; }

        public PostUserCommand(User user)
        {
            this.User = user;
        }

        public void Execute()
        {
            try
            {
               // _logger.LogInformation("Entrando a Execute() PostUserCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                this.User.Id = (dao.AddUser(this.User)).Id;
            }
            catch (Exception e)
            {
               // _logger.LogError("Exception",e);
                Console.WriteLine(e.ToString());
            }
        }

        public Entity GetResult()
        {
            return this.User;
        }
    }
}
