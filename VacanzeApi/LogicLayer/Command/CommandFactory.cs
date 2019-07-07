using System;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using System.Collections.Generic;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo8;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo8;


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

        public static AddHotelCommand createAddHotelCommand(Hotel hotel)
        {
            return new AddHotelCommand(hotel);
        }   
        public static GetHotelByIdCommand GetHotelByIdCommand(int id)
        {
            return new GetHotelByIdCommand(id);
        }  
		public static DeleteHotelCommand DeleteHotelCommand(int id)
        {
            return new DeleteHotelCommand(id);
        }  
		public static UpdateHotelCommand UpdateHotelCommand(int id, Hotel hotel)
        {
            return new UpdateHotelCommand(id,hotel);
        }

        public static AddCruiserCommand CreateAddCruiserCommand(CruiserDTO cruiserDto)
        {
            return new AddCruiserCommand(cruiserDto);
        }

         public static GetCruiserCommand CreateGetCruiserCommand(int id)
        {
            return new GetCruiserCommand(id);
        }

        public static AddLayoverCommand CreateAddLayoverCommand(LayoverDTO layoverDto)
        {
            return new AddLayoverCommand(layoverDto);
        }

        public static DeleteLayoverCommand CreateDeleteLayoverCommand(int id)
        {
            return new DeleteLayoverCommand(id);
        }

        public static DeleteCruiserCommand CreateDeleteCruiserCommand(int id)
        {
            return new DeleteCruiserCommand(id);
        }

        public static UpdateCruiserCommand CreateUpdateCruiserCommand(int id, CruiserDTO cruiserDto)
        {
            return new UpdateCruiserCommand(id, cruiserDto);
        }
    }
}
