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
    public class PostUser_RoleCommand : Command, CommandResult<Entity>
    {
        public Role Role { get; set; }
        public User User { get; set; }

        public PostUser_RoleCommand(User user, Role role)
        {
            this.User = user;
            this.Role = role;
        }

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.AddUser_Role(this.User.Id,this.Role.Id);
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
