using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5{

    public class BrandMapper : Mapper<BrandDTO> {

        public BrandDTO CreateDTO(Entity entity){
            Brand brand =  (Brand) entity;
            BrandDTO brandDTO = DTOFactory.CreateBrandDTO(brand.BrandName);
            return brandDTO;
        }

        public Entity CreateEntity(BrandDTO dto){
            Entity entity = EntityFactory.createBrand(dto.BrandName);
            return entity;
        }

        public List<BrandDTO> CreateDTOList(List<Entity> entities){
            List<BrandDTO> dtos = new List<BrandDTO>();
            foreach(Entity entity in entities){
                Brand brand = (Brand) entity;
                dtos.Add(DTOFactory.CreateBrandDTO(brand.BrandName));
            }
            return dtos;
        }

        public List<Entity> CreateEntityList(List<BrandDTO> dtos){
            List<Entity> entities = new List<Entity>();
            foreach(BrandDTO dto in dtos){
                entities.Add(EntityFactory.createBrand(dto.BrandName));
            }
            return entities;
        }

    }

}
