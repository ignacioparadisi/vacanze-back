using System.Net.Http;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class ValidateBaggageUpdateCommand : Command
    {
        private readonly Baggage _baggage;

        public ValidateBaggageUpdateCommand(Baggage baggage)
        {
            this._baggage = baggage;
        }
        
        public void Execute()
        {
            BaggageValidator.Validate(_baggage,HttpMethod.Put);
        }
    }
}