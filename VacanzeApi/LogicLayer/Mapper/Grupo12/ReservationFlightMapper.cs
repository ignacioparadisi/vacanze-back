using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo12;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo12;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo12
{
    public class ReservationFlightMapper : Mapper<FlightResDTO, FlightRes>
    {
        public FlightResDTO CreateDTO(FlightRes entity){
            FlightRes flightRes = entity;
            FlightResDTO flightResDTO = DTOFactory.CreateFlightResDTO(flightRes._id,flightRes._price,flightRes._timestamp,
            flightRes._seatNum,flightRes._namecityI,flightRes._namecountryI, flightRes._namecityV,flightRes._namecountryV,
            flightRes._numPas, flightRes._id_user);
            return flightResDTO;
        }

        public FlightRes CreateEntity(FlightResDTO flightResDto){
            FlightRes entity = EntityFactory.CreateFlightRes(flightResDto._id,flightResDto._price,flightResDto._timestamp,
            flightResDto._seatNum,flightResDto._namecityI,flightResDto._namecountryI, flightResDto._namecityV,flightResDto._namecountryV,
            flightResDto._numPas,flightResDto._id_user,flightResDto._id_fli);
            
            return entity;
        }

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