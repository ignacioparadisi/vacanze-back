using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Grupo2
{
    public class GetUserByIdCommand : Command
    {

        public Entity user { get; set; }
        public int id { get; set; }
        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            UserDAO users = factory.GetUserDAO();
            user = (User) users.GetUserById(id);
        }

        public GetUserByIdCommand(int id)
        {
            this.id = id;
        }

        public Entity Return()
        {
            return user;
        }
    }
}
