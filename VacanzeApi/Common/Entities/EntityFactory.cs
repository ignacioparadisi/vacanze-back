using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Entity createBrand(string brandName){
            return new Brand(brandName);
        }

    }
}