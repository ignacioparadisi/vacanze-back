using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5
{
    public class ModelMapper : Mapper<ModelDTO,Model>
    {
        public ModelDTO CreateDTO(Model model){
            ModelDTO modelDto = DTOFactory.CreateModelDTO(
                model.Id, model.ModelBrandId, model.ModelName,
                model.Capacity, model.Picture
            );
            return modelDto;
        }

        public Model CreateEntity(ModelDTO dto){
            Model model = EntityFactory.CreateModel(
                dto.Id, dto.BrandId, dto.ModelName,
                dto.Capacity, dto.Picture
            );
            return model;
        }

        public List<ModelDTO> CreateDTOList(List<Model> models){
            List<ModelDTO> dtos = new List<ModelDTO>();
            foreach(Model model in models){
                dtos.Add(DTOFactory.CreateModelDTO(
                    model.Id, model.ModelBrandId, model.ModelName,
                    model.Capacity, model.Picture)
                );
            }
            return dtos;
        }

        public List<Model> CreateEntityList(List<ModelDTO> dtos){
            List<Model> models = new List<Model>();
            foreach(ModelDTO dto in dtos){
                models.Add(EntityFactory.CreateModel(
                    dto.Id, dto.BrandId, dto.ModelName, 
                    dto.Capacity, dto.Picture)
                );
            }
            return models;
        }
    }

}