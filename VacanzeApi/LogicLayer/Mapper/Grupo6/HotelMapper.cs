using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.DTO;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6{

    public class HotelMapper : Mapper<HotelDTO> {

        public HotelDTO CreateDTO(Entity entity){
            Hotel hotel =  (Hotel) entity;
            HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(hotel.Id, hotel.Name, hotel.AmountOfRooms, 
                                            hotel.RoomCapacity , hotel.IsActive, hotel.AddressSpecification,
                                            hotel.PricePerRoom, hotel.Website, hotel.Phone, hotel.Picture,
                                            hotel.Stars, hotel.Location.Id);
            return HotelDTO;
        }

        public Entity CreateEntity(HotelDTO hotelDto){
            Entity entity = EntityFactory.createHotel(hotelDto.Id, hotelDto.Name, hotelDto.AmountOfRooms, 
                                            hotelDto.RoomCapacity, hotelDto.IsActive, hotelDto.AddressSpecification,
                                            hotelDto.PricePerRoom, hotelDto.Website, hotelDto.Phone ,
                                            hotelDto.Picture, hotelDto.Stars, hotelDto.Location.Id);
            
            return entity;
        }

        public List<HotelDTO> CreateDTOList(List<Entity> entities){
            List<HotelDTO> dtos = new List<HotelDTO>();
            foreach(Entity entity in entities){
                Hotel hotel = (Hotel) entity;
                dtos.Add(DTOFactory.CreateHotelDTO(hotel.Id, hotel.Name, hotel.AmountOfRooms, 
                                            hotel.RoomCapacity , hotel.IsActive, hotel.AddressSpecification,
                                            hotel.PricePerRoom, hotel.Website, hotel.Phone, hotel.Picture,
                                            hotel.Stars, hotel.Location.Id));
            }
            return dtos;
        }

        public List<Entity> CreateEntityList(List<HotelDTO> dtos){
            List<Entity> entities = new List<Entity>();
            foreach(HotelDTO hotelDto in dtos){
                entities.Add(EntityFactory.createHotel(hotelDto.Id, hotelDto.Name, hotelDto.AmountOfRooms, 
                                            hotelDto.RoomCapacity, hotelDto.IsActive, hotelDto.AddressSpecification,
                                            hotelDto.PricePerRoom, hotelDto.Website, hotelDto.Phone ,
                                            hotelDto.Picture, hotelDto.Stars, hotelDto.Location.Id));
            }
            return entities;
        }

    }

}
