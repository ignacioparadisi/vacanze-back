using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;

namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Brand createBrand(string brandName){
            return new Brand(brandName);
        }

        public static Restaurant CreateRestaurant(int id, string name, int capacity,bool isActive,decimal qualify, string specialty, 
            decimal price, string businessName, string picture, 
            string description, string phone, int location, string address)
        {
            return new Restaurant(id,name,capacity,isActive,qualify,specialty,price,businessName,picture,description,phone,location,address);
        }
    }
}