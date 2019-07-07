using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class AddBrandCommand : CommandResult<int> 
    {
        private int _id;

        private Brand _brand;
        public Brand Brand { get{ return _brand; } set{ _brand = value; } }

        public AddBrandCommand (Brand _brand) {
            this._brand = _brand;
        }

        public void Execute () {
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IBrandDAO brandDAO = daoFactory.GetBrandDAO();
            _id = brandDAO.AddBrand (_brand);
        }

        public int GetResult () {
            return _id;
        }

    }
}