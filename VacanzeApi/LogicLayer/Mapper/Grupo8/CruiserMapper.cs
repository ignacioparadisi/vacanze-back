/* using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class CruiserMapper : Mapper<CruiserDTO,Cruiser>
    {
        public CruiserDTO CreateDTO(Cruiser cruiser)
        {
            return new CruiserDTO
            {
                Id = cruiser.Id,
                Name = cruiser.Name,
                Status = cruiser.Status,
                Capacity = cruiser.Capacity,
                LoadingShipCap = cruiser.LoadingShipCap,
                Model = cruiser.Model,
                Line = cruiser.Line,
                Picture = cruiser.Picture
            };
        }
        
        public List<CruiserDTO> CreateDTOList(List<Cruiser> cruisers)
        {
            List<CruiserDTO> dtoList= new List<CruiserDTO>();
            foreach (Cruiser cruiser in cruisers)
            {
                dtoList.Add(new CruiserDTO
                {
                    Id = cruiser.Id,
                    Name = cruiser.Name,
                    Status = cruiser.Status,
                    Capacity = cruiser.Capacity,
                    LoadingShipCap = cruiser.LoadingShipCap,
                    Model = cruiser.Model,
                    Line = cruiser.Line,
                    Picture = cruiser.Picture
                });
            }
            return dtoList;
        }

        public Cruiser CreateEntity(CruiserDTO dto)
        {
            return EntityFactory.CreateCruiser(dto.Id,dto.Name,dto.Status,dto.Capacity,dto.LoadingShipCap,
                    dto.Model,dto.Line,dto.Picture);
        }

        public List<Cruiser> CreateEntityList(List<CruiserDTO> dtoList)
        {
            List<Cruiser> cruisers = new List<Cruiser>();
            foreach (CruiserDTO dto in dtoList)
            {
                cruisers.Add(
                     EntityFactory.CreateCruiser(dto.Id,dto.Name,dto.Status,dto.Capacity,dto.LoadingShipCap,
                    dto.Model,dto.Line,dto.Picture));
            }
            return cruisers;
        }
    }
} */