using vacanze_back.VacanzeApi.Common.Entities.Grupo10;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo10;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo10
{
    public class DeleteTravel: Command
    {
        public int id;
        public DeleteTravel( int id ){
            this.id = id;
        }

        public void Execute()
        {
            DAOFactory factory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            ITravel traveldao = factory.GetTravelDAO();
            
            
             int i =  traveldao.Deletetravel(id);
   
 
        }


    }
}