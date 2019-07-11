using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class LayoverMapper : Mapper<LayoverDTO,Layover>
    {
        /// <summary>
        ///     Metodo para convertir una entidad Layover en un LayoverDTO
        /// </summary>
        /// <param name="layover">Objeto que contiene toda la informacion del layover a transformar</param>
        /// <returns>Un objeto de tipo LayoverDTO</returns>
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

        /// <summary>
        ///     Metodo para convertir una lista de Layover en una lista de LayoverDTO
        /// </summary>
        /// <param name="layovers">Objeto que contiene la lista de los layovers a transformar</param>
        /// <returns>Un objeto de tipo lista de LayoverDTO</returns>
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

        /// <summary>
        ///     Metodo para convertir un LayoverDTO en un Layover
        /// </summary>
        /// <param name="dto">Objeto que contiene toda la informacion del LayoverDTO a transformar</param>
        /// <returns>Un objeto de tipo Layover</returns>
        public Layover CreateEntity(LayoverDTO dto)
        {
            return EntityFactory.CreateLayover(dto.Id,dto.CruiserId,dto.DepartureDate,dto.ArrivalDate,dto.Price,dto.LocDeparture,
                    dto.LocArrival);
        }

        /// <summary>
        ///     Metodo para convertir una lista de LayoverDTO en una lista de Layover
        /// </summary>
        /// <param name="dtoList">Objeto que contiene la lista de los layoverDTO a transformar</param>
        /// <returns>Un objeto de tipo lista de Layover</returns>
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