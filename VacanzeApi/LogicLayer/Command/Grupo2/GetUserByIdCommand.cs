using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class GetUserByIdCommand : Command, CommandResult<Entity>
    {

        public Entity User { get; set; }
        public int Id { get; set; }
        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO users = factory.GetUserDAO();
                User = (User)users.GetUserById(Id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public GetUserByIdCommand(int id)
        {
            this.Id = id;
        }
        public Entity GetResult()
        {
            return User;
        }
    }
}
