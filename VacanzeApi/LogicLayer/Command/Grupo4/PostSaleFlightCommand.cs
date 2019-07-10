using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo4
{
    public class PostSaleFlightCommand : Command, CommandResult<int>
    {
        private int _idReturn;
        private List<PostSaleFlight> _postflight;

        public PostSaleFlightCommand(List<PostSaleFlight> postflight)
        {
            _postflight = postflight;

        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);

            _idReturn = daoFactory.PostSaleFlightDAO().PostSaleFlight(_postflight);
        }

        public int GetResult()
        {
            return _idReturn;
        }


    }
}
