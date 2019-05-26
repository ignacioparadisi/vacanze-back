using System;
using System.Collections.Generic;
using Npgsql;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;

namespace vacanze_back.VacanzeApi.Persistence.Connection.Grupo6
{
    public class HotelConnection : Connection
    {
        public HotelConnection()
        {
            CreateStringConnection();
        }

        /// <summary>
        ///     Obtener todos los hoteles
        /// </summary>
        public List<Hotel> GetHotels()
        {
            var hotelList = new List<Hotel>();
            try
            {
                Connect();
                StoredProcedure("consultarHoteles()");
                ExecuteReader();

                for (var i = 0; i < numberRecords; i++)
                {
                    var id = Convert.ToInt32(GetString(i, 0));
                    var name = GetString(i, 1);
                    var capacity = Convert.ToInt32(GetString(i, 2));
                    var status = Convert.ToBoolean(GetString(i, 3));
                    var phone = GetString(i, 4);
                    var web = GetString(i, 5);
                    var hotel = new Hotel(id, name, capacity, status, phone, web);
                    hotelList.Add(hotel);
                }

                return hotelList;
            }
            catch (NpgsqlException)
            {
                throw new DatabaseException("Error con la base de datos al consultar los hoteles");
            }
            catch (Exception e)
            {
                throw new GeneralException(e, DateTime.Now);
            }
        }

        public long AddHotel(Hotel hotel)
        {
            Connect();
            StoredProcedure(
                "addhotel(@name, @amountOfRooms, @active, @phone, @website, @location)");
            AddParameter("name", hotel.Name);
            AddParameter("amountOfRooms", hotel.AmountOfRooms);
            AddParameter("active", hotel.IsActive);
            AddParameter("phone", hotel.Phone);
            AddParameter("website", hotel.Website);
            AddParameter("location", 1); // TODO: Usar id de una ubicacion recibida!!
            ExecuteReader();
            var savedId = GetInt(0, 0);
            return savedId;
        }
    }
}