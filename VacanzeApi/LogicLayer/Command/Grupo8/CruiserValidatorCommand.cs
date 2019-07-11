using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo8;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8
{
    /// <summary>  
    ///  Comando para validar los compos de un Restaurante
    /// </summary> 
    public class CruiserValidatorCommand : Command
    {
        private Cruiser _cruiser;

        public CruiserValidatorCommand(Cruiser cruiser)
        {
            _cruiser = cruiser;
        }
        public void Execute()
        {
            CruiserValidator.Validate(_cruiser);
        }
    }
}