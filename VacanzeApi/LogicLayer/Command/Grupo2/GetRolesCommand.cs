using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class GetRolesCommand : Command, CommandResult<List<Entity>>
    {

        private static List<Entity> Roles;

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                RoleDAO roles = factory.GetRoleDAO();
                Roles = roles.GetRoles();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public List<Entity> GetResult()
        {
            return Roles;
        }
    }
}
