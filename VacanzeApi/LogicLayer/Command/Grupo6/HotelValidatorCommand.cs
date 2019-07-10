using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6 {

    public class HotelValidatorCommand : Command {
        private Hotel _hotel;
        /// <summary>
        ///     recibe el hotele a ser vaidado
        /// </summary>
        /// <param name="_hotel">hotel a ser validado</param>	
        public HotelValidatorCommand (Hotel _hotel) {
            this._hotel = _hotel;
        }
        /// <summary>
        ///     Valida que los campos de un <see cref="Hotel" /> sean correctos, lanzando excepciones
        ///     segun los errores detectados.
        /// </summary>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>  
        public void Execute () {            
            HotelValidator.Validate(_hotel);
        }
    }

}