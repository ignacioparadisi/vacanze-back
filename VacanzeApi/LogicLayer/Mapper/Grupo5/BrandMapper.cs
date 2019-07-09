using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5{

    public class BrandMapper : Mapper<BrandDTO,Brand> {

        public BrandDTO CreateDTO(Brand brand){
            BrandDTO brandDto = DTOFactory.CreateBrandDTO(brand.Id, brand.BrandName);
            return brandDto;
        }

        public Brand CreateEntity(BrandDTO dto){
            Brand brand = EntityFactory.CreateBrand(dto.Id,dto.BrandName);
            return brand;
        }

        public List<BrandDTO> CreateDTOList(List<Brand> brands){
            List<BrandDTO> dtos = new List<BrandDTO>();
            foreach(Brand brand in brands){
                dtos.Add(DTOFactory.CreateBrandDTO(brand.Id, brand.BrandName));
            }
            return dtos;
        }

        public List<Brand> CreateEntityList(List<BrandDTO> dtos){
            List<Brand> brands = new List<Brand>();
            foreach(BrandDTO dto in dtos){
                brands.Add(EntityFactory.CreateBrand(dto.Id, dto.BrandName));
            }
            return brands;
        }

    }

}
