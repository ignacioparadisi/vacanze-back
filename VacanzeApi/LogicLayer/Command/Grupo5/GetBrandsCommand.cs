using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5 {

    public class GetBrandsCommand : Command, CommandResult<List<Brand>> {
        private List<Brand> brands;

        public GetBrandsCommand(){}

        public void Execute(){
            DAOBrand dao = new DAOBrand(); //Without Factory
            brands = dao.GetBrands();
        }

        public List<Brand> GetResult(){
            return brands;
        }
    }

}