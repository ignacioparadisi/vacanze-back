using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9
{
    public class ClaimMapper : Mapper<ClaimDto, Claim>
    {
        public ClaimDto CreateDTO(Claim entity)
        {
            return new ClaimDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                BaggageId = entity.BaggageId
            };
        }

        public Claim CreateEntity(ClaimDto dto)
        {
            return ClaimBuilder.Create()
                .WithTitle(dto.Title)
                .WithDescription(dto.Description)
                .WithBagagge(dto.BaggageId)
                .WithStatus(dto.Status)
                .WithId(dto.Id)
                .Build();
        }

        public List<ClaimDto> CreateDTOList(List<Claim> entities)
        {
            var dtos = new List<ClaimDto>();
            
            foreach (var claim in entities)
                dtos.Add(CreateDTO(claim));
            
            return dtos;
        }

        public List<Claim> CreateEntityList(List<ClaimDto> dtos)
        {
            var claims = new List<Claim>();
            
            foreach (var dto in dtos)
                claims.Add(CreateEntity(dto));
            
            return claims;
        }
    }
}