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
    public class RoleMapper : Mapper<RoleDTO>
    {
        public RoleDTO CreateDTO(Entity entity)
        {
            try
            {
                Role role = (Role)entity;
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

        public List<RoleDTO> CreateDTOList(List<Entity> entities)
        {
            try
            {
                List<RoleDTO> dtos = new List<RoleDTO>();
                foreach (Entity entity in entities)
                {
                    Role role = (Role) entity;
                    dtos.Add(DTOFactory.CreateRoleDTO(role.Id, role.Name));
                }
                return dtos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Entity CreateEntity(RoleDTO dto)
        {
            try
            {
                return EntityFactory.CreateRol(dto._Id, dto._Name);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Entity> CreateEntityList(List<RoleDTO> dtos)
        {
            try
            {
                List<Entity> entities = new List<Entity>();
                foreach (RoleDTO dto in dtos)
                {
                    entities.Add(EntityFactory.CreateRol(dto._Id, dto._Name));
                }
                return entities;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
