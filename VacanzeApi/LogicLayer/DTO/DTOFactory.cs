using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO{

    public class DTOFactory{

        public static BrandDTO CreateBrandDTO(string brandName){
            return new BrandDTO(brandName);
        }
    }

}