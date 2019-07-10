
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities.Grupo14;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo14;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class AddResRestaurantCommand : Command
    {
        private int _id;
        private Restaurant_res _resRestaurant;
        public int Id { get { return _id; } set { _id = value; } }
        public Restaurant_res ResRestaurant { get{ return _resRestaurant; } set{ _resRestaurant = value; } }

        //Constructor de la clase AddResRestaurantCommand
        public AddResRestaurantCommand(Restaurant_res _resRestaurant)
        {
            this._resRestaurant = _resRestaurant;
        }

        public void Execute() //Metodo Execute() que se hereda de la interface Command y se sobreescribe.
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);           //Instancia de la fabrica abstracta DAO con el Manejador de BD especifico.
            IReservationRestaurantDAO ResRestDAO = factory.GetReservationRestaurantDAO();   //Se hace instancia al metodo contenido en el DAOFactory.
            ResRestDAO.addReservation(_resRestaurant);                                      //Se hace llamado al metodo que se desea realizar consultar en la base de datos. 
        }
        public int GetResult()
        {
            return _id;
        }
    }
}
