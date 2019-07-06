using System.Collections.Generic;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo7;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo7;

namespace vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo7
{
    /// <summary>  
    ///  Mapper de RestaurantDto a Restaurant Entity 
    /// </summary>
    public class RestaurantMapper : Mapper<RestaurantDto,Restaurant>
    {
        public RestaurantDto CreateDTO(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Capacity = restaurant.Capacity,
                IsActive = restaurant.IsActive,
                Qualify = restaurant.Qualify,
                Specialty = restaurant.Specialty,
                Price = restaurant.Price,
                BusinessName = restaurant.BusinessName,
                Picture = restaurant.Picture,
                Description = restaurant.Description,
                Phone = restaurant.Phone,
                Location = restaurant.Location,
                Address = restaurant.Address
            };
        }
        
        public List<RestaurantDto> CreateDTOList(List<Restaurant> restaurants)
        {
            List<RestaurantDto> dtoList= new List<RestaurantDto>();
            foreach (Restaurant restaurant in restaurants)
            {
                dtoList.Add(new RestaurantDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Capacity = restaurant.Capacity,
                    IsActive = restaurant.IsActive,
                    Qualify = restaurant.Qualify,
                    Specialty = restaurant.Specialty,
                    Price = restaurant.Price,
                    BusinessName = restaurant.BusinessName,
                    Picture = restaurant.Picture,
                    Description = restaurant.Description,
                    Phone = restaurant.Phone,
                    Location = restaurant.Location,
                    Address = restaurant.Address
                });
            }
            return dtoList;
        }

        public Restaurant CreateEntity(RestaurantDto dto)
        {
            return EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                ,dto.Address);
        }

        public List<Restaurant> CreateEntityList(List<RestaurantDto> dtoList)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            foreach (RestaurantDto dto in dtoList)
            {
                restaurants.Add(
                     EntityFactory.CreateRestaurant(dto.Id, dto.Name, dto.Capacity, dto.IsActive, dto.Qualify,
                    dto.Specialty, dto.Price, dto.BusinessName, dto.Picture, dto.Description, dto.Phone, dto.Location
                    ,dto.Address));
            }
            return restaurants;
        }
    }
}