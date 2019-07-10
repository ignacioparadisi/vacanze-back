using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.Persistence.DAO;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class GetResRestaurantByIdCommand : CommandResult<List<Restaurant_res>>
    {
        private readonly int _id;
        private List<Restaurant_res> _result;

        //Constructor de la clase AddResRestaurantCommand
        public GetResRestaurantByIdCommand(int id)
        {
            _id = id;
        }

        public List<Restaurant_res> GetResult()
        {
            return _result;
        }
        
        public void Execute() //Metodo Execute() que se hereda de la interface Command y se sobreescribe.
        {
            var daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);           //Instancia de la fabrica abstracta DAO con el Manejador de BD especifico.
            _result = daoFactory.GetReservationRestaurantDAO().getResRestaurant(_id);   //Se hace el llamado al metodo que se desea cosultar en base de datos pasando un parametro.
        }
    }
}
