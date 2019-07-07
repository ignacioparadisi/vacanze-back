using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.DAO;
using vacanze_back.VacanzeApi.Persistence.DAO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 {

    public class GetBrandsCommand : CommandResult<List<Brand>> {
        private List<Brand> brands;

        public GetBrandsCommand(){}

        public void Execute(){
            DAOFactory daoFactory = DAOFactory.GetFactory(DAOFactory.Type.Postgres);
            IBrandDAO brandDAO = daoFactory.GetBrandDAO();
            brands = brandDAO.GetBrands();
        }

        public List<Brand> GetResult(){
            return brands;
        }
    }

}