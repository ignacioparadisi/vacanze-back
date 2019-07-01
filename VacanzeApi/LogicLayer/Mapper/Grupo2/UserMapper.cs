using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo2;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo2;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo2
{
    public class UserMapper : Mapper<UserDTO>
    {
        public UserDTO CreateDTO(Entity entity)
        {
            try
            {
                User user = (User)entity;
                return DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Email, user.Lastname, user.Name, user.Password, user.Roles);
            }
            catch(NullReferenceException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<UserDTO> CreateDTOList(List<Entity> entities)
        {
            try
            {
                List<UserDTO> dtos = new List<UserDTO>();
                foreach (Entity entity in entities)
                {
                    User user = (User)entity;
                    dtos.Add(DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Email, user.Lastname, user.Name, user.Password, user.Roles));
                }
                return dtos;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Entity CreateEntity(UserDTO dto)
        {
            try
            {
                return EntityFactory.CreateUser(dto.Id, dto.DocumentId, dto.Name, dto.Lastname, dto.Email, dto.Password, dto.Roles);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Entity> CreateEntityList(List<UserDTO> dtos)
        {
            try
            {
                List<Entity> entities = new List<Entity>();
                foreach (UserDTO dto in dtos)
                {
                    entities.Add(EntityFactory.CreateUser(dto.Id,dto.DocumentId,dto.Name,dto.Lastname,dto.Email,dto.Password,dto.Roles));
                }
                return entities;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
