using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using  vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO{

    public class DTOFactory{

        public static BrandDTO CreateBrandDTO(string brandName){
            return new BrandDTO(brandName);
        }

        public static UserDTO CreateUserDTO(int Id, long DocumentId, string Email, string Lastname, string Name, string Password, List<Role> Roles)
        {
            return new UserDTO(Id, DocumentId, Email, Lastname, Name, Password, Roles);
        }

        public static RoleDTO CreateRoleDTO(int id, string name)
        {
            return new RoleDTO(id, name);
        }
    }

}