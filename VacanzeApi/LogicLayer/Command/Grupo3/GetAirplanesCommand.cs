using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;
namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo3
{
    public class GetAirplaneCommand: Command, CommandResult<List<Airplane>>
    {
         List<Airplane> airplanes = new List<Airplane>();

        public int _id;
        public GetAirplaneCommand () {
        }

        public void Execute()
        {
            try
            {
               
                Console.WriteLine("id="+_id);
                DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
                IAirplaneDAO airplaneDao = factory.GetAirplane();
                airplanes=airplaneDao.Get();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public List<Airplane> GetResult()
        {
            return airplanes;
        }
    }
}
