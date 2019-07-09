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
    public class RoleMapper : Mapper<RoleDTO,Role>
    {
        public RoleDTO CreateDTO(Role role)
        {
            try
            {
                return DTOFactory.CreateRoleDTO(role.Id, role.Name);
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RoleDTO> CreateDTOList(List<Role> roles)
        {
            try
            {
                List<RoleDTO> dtos = new List<RoleDTO>();
                foreach (Role role in roles)
                {
                    dtos.Add(DTOFactory.CreateRoleDTO(role.Id, role.Name));
                }
                return dtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Role CreateEntity(RoleDTO dto)
        {
            try
            {
                return (Role) EntityFactory.CreateRol(dto.Id, dto.Name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Role> CreateEntityList(List<RoleDTO> dtos)
        {
            try
            {
                List<Role> roles = new List<Role>();
                foreach (RoleDTO dto in dtos)
                {
                    roles.Add((Role) EntityFactory.CreateRol(dto.Id, dto.Name));
                }
                return roles;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
