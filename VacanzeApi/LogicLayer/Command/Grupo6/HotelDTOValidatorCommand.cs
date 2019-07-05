using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class HotelDTOValidatorCommand : Command {
        private HotelDTO _hotel;

        public HotelDTOValidatorCommand (HotelDTO _hotel) {
            this._hotel = _hotel;
        }

        public void Execute () {            
            HotelValidator.Validate(_hotel);
        }
    }

}