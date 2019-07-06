using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7
{
    /// <summary>  
    ///  Comando para validar los compos de un Restaurante
    /// </summary> 
    public class RestaurantValidatorCommand : Command
    {
        private Restaurant _restaurant;

        public RestaurantValidatorCommand(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }
        public void Execute()
        {
            RestaurantValidator.Validate(_restaurant);
        }
    }
}