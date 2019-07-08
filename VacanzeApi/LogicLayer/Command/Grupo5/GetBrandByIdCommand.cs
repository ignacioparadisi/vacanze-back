using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 
{
    public class GetBrandByIdCommand : CommandResult<Brand> 
    {
        private int _brandId;

        private Brand _brand;
        public Brand Brand { get{ return _brand; } set{ _brand = value; } }


        public GetBrandByIdCommand(int _brandId){
            this._brandId = _brandId;
        }

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IBrandDAO brandDAO = daoFactory.GetBrandDAO();
            _brand = brandDAO.GetBrandById(_brandId);
        }

        public Brand GetResult(){
            return _brand;
        }
    }
}