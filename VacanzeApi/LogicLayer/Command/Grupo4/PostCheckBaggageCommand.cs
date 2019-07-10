using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo4
{
    public class PostCheckBaggageCommand:Command, CommandResult<int>
    {

        private int _idReturn;

        private List<CheckinBaggage> _checkBag;

        public PostCheckBaggageCommand(List<CheckinBaggage> checkBag)
        {
            _checkBag = checkBag;

        }

        public void Execute()
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);

            _idReturn = daoFactory.PostCheckBaggage().PostCheckBaggage(_checkBag);
        }

        public int GetResult()
        {
            return _idReturn;
        }
    }
}
