using System.Net.Http;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class ValidateClaimUpdateCommand : Command
    {
        private readonly Claim _claim;

        public ValidateClaimUpdateCommand(Claim claim)
        {
            _claim = claim;
        }

        public void Execute()
        {
            ClaimValidator.Validate(_claim, HttpMethod.Put);
        }
    }
}