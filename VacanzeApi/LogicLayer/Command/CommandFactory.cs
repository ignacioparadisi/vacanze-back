using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo9;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo9;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Command.Locations;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo4;
using System;
using vacanze_back.VacanzeApi.Common.Entities.Grupo4;
using System.Collections.Generic;

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

        public static DeleteClaimByIdCommand CreateDeleteClaimByIdCommand(int id)
        {
            return new DeleteClaimByIdCommand(id);
        }

        public static UpdateClaimCommand CreateUpdateClaimCommand(int id, Claim fieldsToUpdate)
        {
            return new UpdateClaimCommand(id, fieldsToUpdate);
        }

        public static ValidateClaimUpdateCommand CreateValidateClaimUpdateCommand(Claim claim)
        {
            return new ValidateClaimUpdateCommand(claim);
        }

        public static ValidateClaimCreationCommand CreateValidateClaimCreationCommand(Claim claim)
        {
            return new ValidateClaimCreationCommand(claim);
        }

        public static GetBaggageByPassportCommand CreateGetBaggageByPassportCommand(string passport)
        {
            return new GetBaggageByPassportCommand(passport);
        }
        
        public static GetBaggageByStatusCommand CreateGetBaggageByStatusCommand(string status)
        {
            return new GetBaggageByStatusCommand(status);
        }
        
        public static UpdateBaggageCommand CreateUpdateBaggageCommand(int id, Baggage baggage)
        {
            return new UpdateBaggageCommand(id,baggage);
        }
        
        public static GetBaggageByIdCommand CreateGetBaggageByIdCommand(int id)
        {
            return new GetBaggageByIdCommand(id);
        }

        public static AddHotelCommand createAddHotelCommand(Hotel hotel)
        {
            return new AddHotelCommand(hotel);
        }

        public static GetHotelByIdCommand GetHotelByIdCommand(int id)
        {
            return new GetHotelByIdCommand(id);
        }
        
		public static GetHotelImageCommand GetHotelImageCommand(int id)
        {
            return new GetHotelImageCommand(id);
        }  
        
		public static GetHotelsCommand GetHotelsCommand()
        {
            return new GetHotelsCommand();
        } 
        
		public static GetHotelsByCityCommand GetHotelsByCityCommand(int city)
        {
            return new GetHotelsByCityCommand(city);
        }  
        
		public static DeleteHotelCommand DeleteHotelCommand(int id)
        {
            return new DeleteHotelCommand(id);
        }

        public static UpdateHotelCommand UpdateHotelCommand(int id, Hotel hotel)
        {
            return new UpdateHotelCommand(id, hotel);
        }
        
        public static AddLocationCommand createAddLocationCommand(Location location)
        {
            return new AddLocationCommand(location);
        }
        
        public static GetLocationByIdCommand GetLocationByIdCommand(int id)
        {
            return new GetLocationByIdCommand(id);
        }  
        
		public static DeleteLocationCommand DeleteLocationCommand(int id)
        {
            return new DeleteLocationCommand(id);
        }  
        
        public static GetLocationsCommand GetLocationsCommand()
        {
            return new GetLocationsCommand();
        }  
        
        public static GetCountriesCommand GetCountriesCommand()
        {
            return new GetCountriesCommand();
        }  
        
        public static GetCitiesByCountryCommand GetCitiesByCountryCommand(int id)
        {
            return new GetCitiesByCountryCommand(id);
        }

        public static GetSaleFlightCommand GetSaleFlightCommand(int origin, int destination, DateTime dateArrival, DateTime dateDeparute)
        {
            return new GetSaleFlightCommand(origin,destination,dateArrival,dateDeparute);
        }
        public static PostSaleFlightCommand PostSaleFlightCommand(List<PostSaleFlight> postflight)
        {
            return new PostSaleFlightCommand(postflight);
        }

    }
}
