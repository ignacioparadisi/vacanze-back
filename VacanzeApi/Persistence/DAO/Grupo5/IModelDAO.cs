using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5{
    
    public interface IModelDAO{
        int AddModel(Model model);
        List<Model> GetModels();
        List<Model> GetModelsByBrand(int brandId);
        bool UpdateModel(Model model);
    }
}