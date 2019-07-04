using System;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.Common.Entities;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper{

    public class LocationMapper : Mapper<LocationDTO, Location> {

        public LocationDTO CreateDTO(Location entity){
                        Console.WriteLine("matantmem" +entity.Id);
            Location location =  (Location) entity;
            LocationDTO LocationDTO = DTOFactory.CreateLocationDTO(location.Id, location.Country, location.City);
            return LocationDTO;
        }

        public Location CreateEntity(LocationDTO locationDto){
            Location entity = EntityFactory.CreateLocation(locationDto.Id, locationDto.Country, locationDto.City);
            
            return entity;
        }

        public List<LocationDTO> CreateDTOList(List<Location> entities){
            List<LocationDTO> dtos = new List<LocationDTO>();
            foreach(Entity entity in entities){
                Location location = (Location) entity;
                dtos.Add(DTOFactory.CreateLocationDTO(location.Id, location.Country, location.City));
            }
            return dtos;
        }

        public List<Location> CreateEntityList(List<LocationDTO> dtos){
            List<Location> entities = new List<Location>();
            foreach(LocationDTO locationDto in dtos){
                entities.Add(EntityFactory.CreateLocation(locationDto.Id, locationDto.Country, locationDto.City));
            }
            return entities;
        }

    }

}
