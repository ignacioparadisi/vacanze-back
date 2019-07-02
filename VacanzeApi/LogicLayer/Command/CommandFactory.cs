using System;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9;

namespace vacanze_back.VacanzeApi.LogicLayer.Command
{
    public class CommandFactory
    {

        public static AddBrandCommand createAddBrandCommand(Brand brand)
        {
            return new AddBrandCommand(brand);
        }

        public static GetBrandsCommand createGetBrandsCommand()
        {
            return new GetBrandsCommand();
        }

        public static GetRestaurantCommand CreateGetRestaurantCommand(int id)
        {
            return new GetRestaurantCommand(id);
        }

        public static AddRestaurantCommand CreateAddRestaurantCommand(RestaurantDTO restaurantDto)
        {
            return new AddRestaurantCommand(restaurantDto);
        }

        public static GetClaimByIdCommand CreateGetClaimByIdCommand(int claimId)
        {
            return new GetClaimByIdCommand(claimId);
        }

        public static GetClaimsByStatusCommand CreateGetClaimsByStatusCommand(string status)
        {
            return new GetClaimsByStatusCommand(status);
        }

        public static GetClaimsByDocumentCommand CreateGetClaimsByDocumentCommand(string document)
        {
            return new GetClaimsByDocumentCommand(document);
        }

        public static AddClaimCommand CreateAddClaimCommand(Claim claim)
        {
            return new AddClaimCommand(claim);
        }

        public static ValidateClaimUpdateCommand CreateValidateClaimUpdateCommand(Claim claim)
        {
            return new ValidateClaimUpdateCommand(claim);
        }

        public static ValidateClaimCreationCommand CreateValidateClaimCreationCommand(Claim claim)
        {
            return new ValidateClaimCreationCommand(claim);
        }
    }
}
