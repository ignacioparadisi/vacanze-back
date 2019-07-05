using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class HotelValidatorCommand : Command {
        private Hotel _hotel;

        public HotelValidatorCommand (Hotel _hotel) {
            this._hotel = _hotel;
        }

        public void Execute () {            
            HotelValidator.Validate(_hotel);
        }
    }

}