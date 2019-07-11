using System;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3
{
    public class GetAirplaneByIdCommand: Command, CommandResult<Entity>
    {

        private int _id;
         Entity i;
        public GetAirplaneByIdCommand (int _id) {
            this._id = _id;
        }

        public void Execute()
        {
            try
            {
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IAirplaneDAO airplaneDao = factory.GetAirplane();
                i = airplaneDao.Find(_id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Entity GetResult()
        {
            return i;
        }
    }
}
