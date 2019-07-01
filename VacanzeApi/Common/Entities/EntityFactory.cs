using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
namespace vacanze_back.VacanzeApi.Common.Entities{

    public class EntityFactory{

        public static Entity createBrand(string brandName){
            return new Brand(brandName);
        }

        public static Entity CreateUser(int id, long documentId, string name, string lastname, string email,
            string password, List<Role> roles)
        {
            return new User(id, documentId, name, lastname, email,password,roles);
        }

        public static Entity CreateRol(int id, string name)
        {
            return new Role(id, name);
        }

    }
}