using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo14;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo14
{
    public class DeleteResRestaurantCommand : Command
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        //Constructor de la clase DeleteResRestaurantCommand
        public DeleteResRestaurantCommand(int id)
        {
            _id = id;
        }

        public void Execute() //Metodo Execute() que se hereda de la interface Command y se sobreescribe.
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);               //Instancia de la fabrica abstracta DAO con el Manejador de BD especifico.
            IReservationRestaurantDAO ResRestDAO = factory.GetReservationRestaurantDAO();       //Se hace instancia al metodo contenido en el DAOFactory.
            ResRestDAO.deleteResRestaurant(_id);                                                //Se hace llamado al metodo que se desea realizar la eliminacion de reserva en la base de datos. 
        }
    }
}
