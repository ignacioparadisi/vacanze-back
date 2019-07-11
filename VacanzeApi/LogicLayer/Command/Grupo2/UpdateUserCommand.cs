using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;
using Microsoft.Extensions.Logging;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class UpdateUserCommand : Command, CommandResult<int>
    {

       // private readonly ILogger _logger;
        public User User { get; set; }
        public int Id { get; set; }

        public UpdateUserCommand(User user, int id)
        {
            User = user;
            Id = id;
        }

        public void Execute()
        {
            try
            {
               // _logger.LogInformation("Entrando a Execute() UpdateUserCommand");
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.UpdateUser(User, Id);
            }
            catch (Exception e)
            {
               // _logger.LogError("Exception",e);
                throw new EmailRequiredException("Problema con el email");
            }
        }

        public int GetResult()
        {
            return Id;
        }
    }
}
