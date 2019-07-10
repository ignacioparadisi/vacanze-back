using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo2
{
    public class DeleteUserByIdCommand : Command, CommandResult<int>
    {
        public int Id { get; set; }
        public DeleteUserByIdCommand(int id)
        {
            this.Id = id;
        }
        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                UserDAO dao = factory.GetUserDAO();
                dao.DeleteUserById(Id);
            }
            catch (Exception e)
            {
                throw new UserNotFoundException("Usuario no encontrado");
            }
        }

        public int GetResult()
        {
            return this.Id;
        }
    }
}
