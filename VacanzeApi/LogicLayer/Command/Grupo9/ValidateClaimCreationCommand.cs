using System.Net.Http;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9
{
    public class ValidateClaimCreationCommand : Command
    {
        private readonly Claim _claim;

        public ValidateClaimCreationCommand(Claim claim)
        {
            _claim = claim;
        }

        public void Execute()
        {
            ClaimValidator.Validate(_claim, HttpMethod.Post);
        }
    }
}