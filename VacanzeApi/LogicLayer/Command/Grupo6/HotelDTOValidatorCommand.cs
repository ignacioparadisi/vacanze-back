using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class HotelDTOValidatorCommand : Command {
        private HotelDTO _hotel;
        /// <summary>
        ///     recibe el hotele a ser vaidado
        /// </summary>
        /// <param name="_hotel">hotel a ser validado</param>		
        public HotelDTOValidatorCommand (HotelDTO _hotel) {
            this._hotel = _hotel;
        }
        /// <summary>
        ///     Valida que los campos de un <see cref="Hotel" /> sean correctos, lanzando excepciones
        ///     segun los errores detectados.
        /// </summary>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        public void Execute () {            
            HotelValidator.Validate(_hotel);
        }
    }

}