
﻿using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        public UserDTO CreateDTO(User user)
        {
            try
            {
                _logger.LogInformation("Entrando a CreateDTO", user);
                _logger.LogDebug("User", user);
                return DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Name, user.Lastname, user.Email, user.Password, user.Roles);
            }
            catch(NullReferenceException e)
            {
                _logger.LogWarning("Null Reference Exception", e);
                throw e;
            }
            catch(Exception e)
            {
                _logger.LogError("Error", e);
                throw e;
            }
        }

        public List<UserDTO> CreateDTOList(List<User> users)
        {
            try
            {
                _logger.LogInformation("Entrando a CreateDTOList", users);
                List<UserDTO> dtos = new List<UserDTO>();
                foreach (User user in users)
                {
                    dtos.Add(DTOFactory.CreateUserDTO(user.Id, user.DocumentId, user.Name, user.Lastname, user.Email, user.Password, user.Roles));
                }
                _logger.LogDebug("User", dtos);
                _logger.LogInformation("Saliendo de CreateDTOList", users);
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
                _logger.LogInformation("Entrando a CreateEntity", dto);
                _logger.LogDebug("UserDTO", dto);
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
                _logger.LogInformation("Entrando a CreateEntityList", dtos);
                List<User> users = new List<User>();
                foreach (UserDTO dto in dtos)
                {
                    users.Add(EntityFactory.CreateUser(dto.Id,dto.DocumentId,dto.Name,dto.Lastname,dto.Email,dto.Password,dto.Roles));
                }
                _logger.LogDebug("Users", users);
                return users;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
