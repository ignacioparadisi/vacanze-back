using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;


namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6{

    public class HotelMapper : Mapper<HotelDTO, Hotel> {
        /// <summary>
        ///     Metodo para crear un DTO a partir de una entidad
        /// </summary>
        /// <param name="entity">entidad de tipo hotel a ser convertida</param>
        /// <returns>un Hotel de tipo DTO</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        public HotelDTO CreateDTO(Hotel entity){
            HotelValidatorCommand command =  CommandFactory.HotelValidatorCommand(entity);
            command.Execute();
            Hotel hotel =  (Hotel) entity;
            HotelDTO HotelDTO = DTOFactory.CreateHotelDTO(hotel.Id, hotel.Name, hotel.AmountOfRooms, 
                                            hotel.RoomCapacity , hotel.IsActive, hotel.AddressSpecification,
                                            hotel.PricePerRoom, hotel.Website, hotel.Phone, hotel.Picture,
                                            hotel.Stars, hotel.Location.Id);
            return HotelDTO;
        }
        /// <summary>
        ///     Metodo para crear una Entidad  a partir de una DTO
        /// </summary>
        /// <param name="hotelDto">DTO de tipo hotel a ser convertida</param>
        /// <returns>un Hotel de tipo Entitidad</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        public Hotel CreateEntity(HotelDTO hotelDto){
            HotelDTOValidatorCommand command =  CommandFactory.HotelDTOValidatorCommand(hotelDto);
            command.Execute();
            Hotel entity = EntityFactory.CreateHotel(hotelDto.Id, hotelDto.Name, hotelDto.AmountOfRooms, 
                                            hotelDto.RoomCapacity, hotelDto.IsActive, hotelDto.AddressSpecification,
                                            hotelDto.PricePerRoom, hotelDto.Website, hotelDto.Phone ,
                                            hotelDto.Picture, hotelDto.Stars, hotelDto.Location.Id);
            
            return entity;
        }
        /// <summary>
        ///     Metodo para crear una lista de DTO a partir de una lista de hoteles entidad
        /// </summary>
        /// <param name="entities">ista entidad de tipo hotel a ser convertida</param>
        /// <returns>Lista de Hoteles de tipo DTO</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        public List<HotelDTO> CreateDTOList(List<Hotel> entities){
            List<HotelDTO> dtos = new List<HotelDTO>();
            foreach(Entity entity in entities){
                Hotel hotel = (Hotel) entity;            
                HotelValidatorCommand command =  CommandFactory.HotelValidatorCommand(hotel);
                command.Execute();
                dtos.Add(DTOFactory.CreateHotelDTO(hotel.Id, hotel.Name, hotel.AmountOfRooms, 
                                            hotel.RoomCapacity , hotel.IsActive, hotel.AddressSpecification,
                                            hotel.PricePerRoom, hotel.Website, hotel.Phone, hotel.Picture,
                                            hotel.Stars, hotel.Location.Id));
            }
            return dtos;
        }
        /// <summary>
        ///     Metodo para crear una lista Entidad  a partir de una lista DTO
        /// </summary>
        /// <param name="dtos">lista deto de tipo hotel a ser convertida</param>
        /// <returns>Lista de Hotel de tipo Entitidad</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        public List<Hotel> CreateEntityList(List<HotelDTO> dtos){
            List<Hotel> entities = new List<Hotel>();
            foreach(HotelDTO hotelDto in dtos){
                HotelDTOValidatorCommand command =  CommandFactory.HotelDTOValidatorCommand(hotelDto);
                command.Execute();
                entities.Add(EntityFactory.CreateHotel(hotelDto.Id, hotelDto.Name, hotelDto.AmountOfRooms, 
                                            hotelDto.RoomCapacity, hotelDto.IsActive, hotelDto.AddressSpecification,
                                            hotelDto.PricePerRoom, hotelDto.Website, hotelDto.Phone ,
                                            hotelDto.Picture, hotelDto.Stars, hotelDto.Location.Id));
            }
            return entities;
        }
    }

}
