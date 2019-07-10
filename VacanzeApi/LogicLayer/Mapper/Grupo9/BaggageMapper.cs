using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9
{
    public class BaggageMapper : Mapper<BaggageDTO,Baggage>
    {
        public BaggageDTO CreateDTO(Baggage entity)
        {
            return new BaggageDTO()
            {
                Id = entity.Id,
                Description = entity.Description,
                Status = entity.Status
            };
        }

        public Baggage CreateEntity(BaggageDTO dto)
        {
            return BaggageBuilder.Create()
                .WithDescription(dto.Description)
                .WithStatus(dto.Status)
                .WithId(dto.Id)
                .Build();
        }

        public List<BaggageDTO> CreateDTOList(List<Baggage> entities)
        {
            var dtos = new List<BaggageDTO>();
            
            foreach (var baggage in entities)
                dtos.Add(CreateDTO(baggage));
            
            return dtos;
        }

        public List<Baggage> CreateEntityList(List<BaggageDTO> dtos)
        {
            var baggages = new List<Baggage>();
            
            foreach (var dto in dtos)
                baggages.Add(CreateEntity(dto));
            
            return baggages;
        }
    }
}