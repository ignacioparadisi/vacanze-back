//El DTO se usa para transferir varios atributos entre cliente-servidor
//Recibo DTO y mando DTO
//Pongo lo mismo que esta en el Entity
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;
using System.Collections.Generic;

namespace vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1
{
    public class LoginDTO:DTO{
        
        public int id{get; set;}
        public List<RoleDTO> roles{get; set;}
        public string email { get; set; }
        public string password { get; set; }

    }
}