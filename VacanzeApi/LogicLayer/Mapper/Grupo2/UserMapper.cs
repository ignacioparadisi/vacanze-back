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
    public class UserMapper : Mapper<UserDTO,User>
    {
        public UserDTO CreateDTO(User user)
        {
            try
            {
                return DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Name, user.Lastname, user.Email, user.Password, user.Roles);
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

        public List<UserDTO> CreateDTOList(List<User> users)
        {
            try
            {
                List<UserDTO> dtos = new List<UserDTO>();
                foreach (User user in users)
                {
                    dtos.Add(DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Name, user.Lastname, user.Email, user.Password, user.Roles));
                }
                return dtos;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public User CreateEntity(UserDTO dto)
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

        public List<User> CreateEntityList(List<UserDTO> dtos)
        {
            try
            {
                List<User> users = new List<User>();
                foreach (UserDTO dto in dtos)
                {
                    users.Add(EntityFactory.CreateUser(dto.Id,dto.DocumentId,dto.Name,dto.Lastname,dto.Email,dto.Password,dto.Roles));
                }
                return users;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
