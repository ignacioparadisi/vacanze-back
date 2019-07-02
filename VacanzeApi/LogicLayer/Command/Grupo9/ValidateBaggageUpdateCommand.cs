using vacanze_back.VacanzeApi.Common.Entities.Grupo9;

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
            //TODO: Llammar al validator de Baggage con el http method Update
            throw new System.NotImplementedException();
        }
    }
}