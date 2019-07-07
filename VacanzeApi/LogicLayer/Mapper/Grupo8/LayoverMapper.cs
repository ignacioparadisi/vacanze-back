using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class LayoverMapper : Mapper<LayoverDTO,Layover>
    {
        public LayoverDTO CreateDTO(Layover layover)
        {
            return new LayoverDTO
            {
                Id = layover.Id,
                CruiserId = layover.CruiserId,
                DepartureDate = layover.DepartureDate,
                ArrivalDate = layover.ArrivalDate,
                Price = layover.Price,
                LocDeparture = layover.LocDeparture,
                LocArrival = layover.LocArrival
            };
        }
        
        public List<LayoverDTO> CreateDTOList(List<Layover> layovers)
        {
            List<LayoverDTO> dtoList= new List<LayoverDTO>();
            foreach (Layover layover in layovers)
            {
                dtoList.Add(new LayoverDTO
                {
                    Id = layover.Id,
                    CruiserId = layover.CruiserId,
                    DepartureDate = layover.DepartureDate,
                    ArrivalDate = layover.ArrivalDate,
                    Price = layover.Price,
                    LocDeparture = layover.LocDeparture,
                    LocArrival = layover.LocArrival
                });
            }
            return dtoList;
        }

        public Layover CreateEntity(LayoverDTO dto)
        {
            return EntityFactory.CreateLayover(dto.Id,dto.CruiserId,dto.DepartureDate,dto.ArrivalDate,dto.Price,dto.LocDeparture,
                    dto.LocArrival);
        }

        public List<Layover> CreateEntityList(List<LayoverDTO> dtoList)
        {
            List<Layover> layovers = new List<Layover>();
            foreach (LayoverDTO dto in dtoList)
            {
                layovers.Add(
                     EntityFactory.CreateLayover(dto.Id,dto.CruiserId,dto.DepartureDate,dto.ArrivalDate,dto.Price,dto.LocDeparture,
                    dto.LocArrival));
            }
            return layovers;
        }
    }
}