using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;

namespace vacanze_back.VacanzeApi.Persistence.DAO.Grupo5{
    
    public interface IBrandDAO{
        int AddBrand(Brand brand);
        Brand GetBrandById(int brandId);
        List<Brand> GetBrands();
        bool UpdateBrand(Brand brand);
    }
}