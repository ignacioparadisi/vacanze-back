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
    public class PostUserCommand : Command, CommandResult<Entity>
    {
        public User User { get; set; }

        public PostUserCommand(User user)
        {
            this.User = user;
        }

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.AddUser(this.User);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Entity GetResult()
        {
            return this.User;
        }
    }
}
