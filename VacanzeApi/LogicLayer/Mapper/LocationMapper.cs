using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper{

    public class LocationMapper : Mapper<LocationDTO, Location> {
        /// <summary>
        ///     Metodo para crear un DTO   a partir de una entidad
        /// </summary>
        /// <param name="entity">Location a ser convertida</param>
        /// <returns>location de tipo DTO</returns>
        public LocationDTO CreateDTO(Location entity){
                        Console.WriteLine("matantmem" +entity.Id);
            Location location =  (Location) entity;
            LocationDTO LocationDTO = DTOFactory.CreateLocationDTO(location.Id, location.Country, location.City);
            return LocationDTO;
        }
        /// <summary>
        ///     Metodo para crear una Entidad a partir de un DTO
        /// </summary>
        /// <param name="locationDto">Location a ser convertida</param>
        /// <returns>location de tipo entidad</returns>
        public Location CreateEntity(LocationDTO locationDto){
            Location entity = EntityFactory.CreateLocation(locationDto.Id, locationDto.Country, locationDto.City);
            
            return entity;
        }
        /// <summary>
        ///     Metodo para crear una lista DTO a partir de una lista entidad
        /// </summary>
        /// <param name="entities">ista Location a ser convertida</param>
        /// <returns>lista location de tipo DTO</returns>
        public List<LocationDTO> CreateDTOList(List<Location> entities){
            List<LocationDTO> dtos = new List<LocationDTO>();
            foreach(Entity entity in entities){
                Location location = (Location) entity;
                dtos.Add(DTOFactory.CreateLocationDTO(location.Id, location.Country, location.City));
            }
            return dtos;
        }
        /// <summary>
        ///     Metodo para crear una lista Entidad a partir de una lista DTO
        /// </summary>
        /// <param name="dtos">ista Location a ser convertida</param>
        /// <returns>lista location de tipo entidad</returns>
        public List<Location> CreateEntityList(List<LocationDTO> dtos){
            List<Location> entities = new List<Location>();
            foreach(LocationDTO locationDto in dtos){
                entities.Add(EntityFactory.CreateLocation(locationDto.Id, locationDto.Country, locationDto.City));
            }
            return entities;
        }

    }

}
