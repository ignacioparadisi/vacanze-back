using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12
{
    public class ReservationFlightMapper : Mapper<FlightResDTO, FlightRes>
    {

        // Convierte objeto de reserva de vuelo a un dto de reserva de vuelo
        /// <param name="entity">Objeto FlightRes </param>
        /// <returns>DTO Object</returns>
        public FlightResDTO CreateDTO(FlightRes entity){
            FlightRes flightRes = entity;
            FlightResDTO flightResDTO = DTOFactory.CreateFlightResDTO(flightRes._id,flightRes._price,flightRes._timestamp,
            flightRes._seatNum,flightRes._namecityI,flightRes._namecountryI, flightRes._namecityV,flightRes._namecountryV,
            flightRes._numPas, flightRes._id_user);
            return flightResDTO;
        }

        // Convierte objeto dto de reserva de vuelo a un objeto de reserva de vuelo
        /// <param name="flightResDto">Objeto FlightResDTO</param>
        /// <returns>FlightRes DTO</returns>
        public FlightRes CreateEntity(FlightResDTO flightResDto){
            FlightRes entity = EntityFactory.CreateFlightRes(flightResDto._id,flightResDto._price,flightResDto._timestamp,
            flightResDto._seatNum,flightResDto._namecityI,flightResDto._namecountryI, flightResDto._namecityV,flightResDto._namecountryV,
            flightResDto._numPas,flightResDto._id_user,flightResDto._id_fli);
            
            return entity;
        }

        // Convierte una lista de objetos dto de reserva de vuelo a una lista de objetos de reserva de vuelo
        /// <param name="entities">Lista de FlightRes</param>
        /// <returns>Lista de DTO</returns>
        public List<FlightResDTO> CreateDTOList(List<FlightRes> entities){
            List<FlightResDTO> dtos = new List<FlightResDTO>();
            foreach(Entity entity in entities){
                FlightRes flightRes = (FlightRes) entity;
                dtos.Add(DTOFactory.CreateFlightResDTO(flightRes._id,flightRes._price,flightRes._timestamp,
                    flightRes._seatNum,flightRes._namecityI,flightRes._namecountryI, flightRes._namecityV,flightRes._namecountryV,
                    flightRes._numPas, flightRes._id_user));
            }
            return dtos;
        }

         // Convierte una lista de objetos de reserva de vuelo a una lista de objetos DTO de reserva de vuelo
        /// <param name="entities">Lista de FlightResDTO</param>
        /// <returns>Lista deFlightRes</returns>
        public List<FlightRes> CreateEntityList(List<FlightResDTO> dtos){
            List<FlightRes> entities = new List<FlightRes>();
            foreach(FlightResDTO flightResDto in dtos){
                entities.Add(EntityFactory.CreateFlightRes(flightResDto._id,flightResDto._price,flightResDto._timestamp,
                            flightResDto._seatNum,flightResDto._namecityI,flightResDto._namecountryI, flightResDto._namecityV,flightResDto._namecountryV,
                            flightResDto._numPas, flightResDto._id_user, flightResDto._id_fli));
            }
            return entities;
        }
    }
}