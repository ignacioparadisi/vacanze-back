using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo3;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo3;
using vacanze_back.VacanzeApi.LogicLayer.DTO;


namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo3{

    public class FlightMapper : Mapper<FlightDTO,Flight>  {

        public FlightDTO CreateDTO(Flight entity){
            AirplaneDTO airplaneDto =new AirplaneDTO(); 
            Flight flight =  (Flight) entity;
            FlightDTO FlightDTO = DTOFactory.CreateFlightDTO(airplaneDto,flight.price,flight.departure,
            flight.arrival,flight.loc_departure,flight.loc_arrival);
            return FlightDTO;
        }

        public Flight CreateEntity(FlightDTO flight){
            Airplane airplane =new Airplane(); 
            Flight entity = EntityFactory.CreateFlight(airplane,flight.price,flight.departure, 
            flight.arrival,flight.loc_departure,flight.loc_arrival);
            return entity;
        }

        
        public List<FlightDTO> CreateDTOList(List<Flight> entities){
            return null;
        }

        public List<Flight> CreateEntityList(List<FlightDTO> dtos){
            return null;
        }
        
    }

}
