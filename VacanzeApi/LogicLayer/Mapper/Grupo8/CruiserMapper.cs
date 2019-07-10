using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8
{
    public class CruiserMapper : Mapper<CruiserDTO,Cruiser>
    {
        /// <summary>
        ///     Metodo para convertir una entidad Cruiser en un CruiserDTO
        /// </summary>
        /// <param name="cruiser">Objeto que contiene toda la informacion del cruiser a transformar</param>
        /// <returns>Un objeto de tipo CruiserDTO</returns>
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

        /// <summary>
        ///     Metodo para convertir una lista de Cruiser en una lista de CruiserDTO
        /// </summary>
        /// <param name="cruisers">Objeto que contiene la lista de los Cruiser a transformar</param>
        /// <returns>Un objeto de tipo lista de CruiserDTO</returns>
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

        /// <summary>
        ///     Metodo para convertir un CruiserDTO en un Cruiser
        /// </summary>
        /// <param name="dto">Objeto que contiene toda la informacion del CruiserDTO a transformar</param>
        /// <returns>Un objeto de tipo Cruiser</returns>
        public Cruiser CreateEntity(CruiserDTO dto)
        {
            return EntityFactory.CreateCruiser(dto.Id,dto.Name,dto.Status,dto.Capacity,dto.LoadingShipCap,
                    dto.Model,dto.Line,dto.Picture);
        }

        /// <summary>
        ///     Metodo para convertir una lista de CruiserDTO en una lista de Cruiser
        /// </summary>
        /// <param name="dtoList">Objeto que contiene la lista de los CruiserDTO a transformar</param>
        /// <returns>Un objeto de tipo lista de Cruiser</returns>
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
}