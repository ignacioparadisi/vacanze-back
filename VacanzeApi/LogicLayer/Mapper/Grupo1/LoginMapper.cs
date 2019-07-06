using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo1;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo1
{
    public class LoginMapper : Mapper<LoginDTO, Login>
    {
        /// Variable global de la clase LoginMapper para 
        /// poder acceder al Mapper de roles y transformar
        RoleMapper roleObject = new RoleMapper();

        public LoginDTO CreateDTO(Login login)
        {            
            return new LoginDTO{
                id = login.Id,
                roles = roleObject.CreateDTOList(login.roles),
                email = login.email,
                password = login.password
                
            };
        }

        public List<LoginDTO> CreateDTOList(List<Login> logins)
        {
            List<LoginDTO> dtoList = new List<LoginDTO>();
            foreach(Login login in logins){
                dtoList.Add(new LoginDTO{
                    id = login.Id,
                    roles = roleObject.CreateDTOList(login.roles),
                    email = login.email,
                    password = login.password
                });
            }
            return dtoList;
        }

        public Login CreateEntity(LoginDTO loginDTO)
        {
            var roles = roleObject.CreateEntityList(loginDTO.roles);
            return EntityFactory.createLogin(loginDTO.id, roles, loginDTO.email, loginDTO.password);
            
        }

        public List<Login> CreateEntityList(List<LoginDTO> dtoList)
        {
            List<Login> loginEntity = new List<Login>();
            foreach(LoginDTO dtoLogin in dtoList){
                var roles = roleObject.CreateEntityList(dtoLogin.roles);
                loginEntity.Add( EntityFactory.createLogin(dtoLogin.id,
                    roles, dtoLogin.email, dtoLogin.password));
            }

            return loginEntity;
        }
    }
}