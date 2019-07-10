using System;
using System.Collections.Generic;
using System.Data;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo13;

namespace vacanze_back.VacanzeApi.Persistence.Repository.Grupo6
{
    public static class HotelRepository
    {
        /// <summary>
        ///     Metodo para persistir un Hotel.
        /// </summary>
        /// <param name="hotel">Datos a ser guardados</param>
        /// <returns>ID del registro del hotel creado en la base de datos</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        public static int AddHotel(Hotel hotel)
        {
            //HotelValidator.Validate(hotel);
            var table = PgConnection.Instance.ExecuteFunction(
                "addhotel(@name, @amountOfRooms, @capacityPerRoom, @active, @addressSpecs, " +
                "@roomPrice, @website, @phone, @picture, @stars, @location)",
                hotel.Name,
                hotel.AmountOfRooms,
                hotel.RoomCapacity,
                hotel.IsActive,
                hotel.AddressSpecification,
                hotel.PricePerRoom,
                hotel.Website ?? "",
                hotel.Phone ?? "",
                hotel.Picture ?? "",
                hotel.Stars,
                hotel.Location.Id
            );
            var savedId = Convert.ToInt32(table.Rows[0][0]);
            return savedId;
        }

        /// <summary>
        ///     Metodo para obtener todos los hoteles guardados.
        /// </summary>
        /// <returns>Lista de hoteles</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public static List<Hotel> GetHotels()
        {
            var table = PgConnection.Instance.ExecuteFunction("getHotels()");
            var hotelList = new List<Hotel>();
            for (var i = 0; i < table.Rows.Count; i++)
                hotelList.Add(ExtractHotelFromRow(table.Rows[i]));
            return hotelList;
        }

        /// <summary>
        ///     Metodo para obtener objeto Hotel correspondiente a los datos guardados para el ID recibido.
        /// </summary>
        /// <param name="id">ID del hotel del cual se quiere un objeto</param>
        /// <returns>Objeto Hotel correspondiente al ID recibido</returns>
        /// <exception cref="HotelNotFoundException">Lanzada si no existe un Hotel para el ID recibido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public static Hotel GetHotelById(int id)
        {
            var resultTable = PgConnection.Instance.ExecuteFunction("GetHotelById(@p_id)", id);
            if (resultTable.Rows.Count == 0)
                throw new HotelNotFoundException(id);
            return ExtractHotelFromRow(resultTable.Rows[0]);
        }

        /// <summary>
        ///     Metodo para listar hoteles por Location.
        /// </summary>
        /// <param name="city">ID del Location de donde se quiere conocer sus hoteles</param>
        /// <returns>
        ///     Lista de hoteles ubicados en la ciudad recibida, si no hay hoteles en la ciudad, la lista
        ///     estaria vacia
        /// </returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public static List<Hotel> GetHotelsByCity(int city)
        {
            var table = PgConnection.Instance.ExecuteFunction("GetHotelsByCity(@city_id)", city);
            var hotelList = new List<Hotel>();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var hotel = ExtractHotelFromRow(table.Rows[i]);
                hotel.AvailableRooms =
                    ReservationRoomRepository.GetAvailableRoomReservations(hotel.Id);
                hotelList.Add(hotel);
            }

            return hotelList;
        }

        /// <summary>
        ///     Metodo para eliminar un hotel permanentemente.
        /// </summary>
        /// <param name="id">ID del hotel a eliminar</param>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public static void DeleteHotel(int id)
        {
            PgConnection.Instance.ExecuteFunction("DeleteHotel(@id)", id);
        }

        /// <summary>
        ///     Metodo para actualizar los datos de un hotel.
        /// </summary>
        /// <param name="id">ID del hotel a actualizar</param>
        /// <param name="newData">
        ///     Objeto Hotel de donde se obtendran los datos para la actualizacion.
        ///     Este objeto debe pasar la validacion del metodo <see cref="Hotel.Validate()" />
        ///     de manera que si solo se quiere actualizar un par de campos, igual se debe que mandar
        ///     el objeto completo y valido, sin cambiar los atributos que no quieras actualizar.
        /// </param>
        /// <returns>Objeto Hotel con los campos actualizados</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la base de
        ///     datos
        /// </exception>
        public static Hotel UpdateHotel(int id, Hotel newData)
        {
            //HotelValidator.Validate(newData);
            PgConnection.Instance.ExecuteFunction(
                "updatehotel(@_id, @name, @amountOfRooms, @capacityPerRoom, @active, @addressSpecs, " +
                "@roomPrice, @website, @phone, @picture, @stars, @location)",
                id,
                newData.Name,
                newData.AmountOfRooms,
                newData.RoomCapacity,
                newData.IsActive,
                newData.AddressSpecification,
                newData.PricePerRoom,
                newData.Website ?? "",
                newData.Phone ?? "",
                newData.Picture ?? "",
                newData.Stars,
                newData.Location.Id
            );
            return GetHotelById(id);
        }

        /// <summary>
        ///     Devuelve la imagen en base64 del hotel.
        /// </summary>
        /// <param name="id">Id del hotel del que se quiere su imagen</param>
        /// <returns>Imagen en base64</returns>
        /// <exception cref="DatabaseException">
        ///     Lanzada si ocurre un fallo al ejecutar la funcion en la bse de
        ///     datos
        /// </exception>
        public static string GetHotelImage(int id)
        {
            var result = PgConnection.Instance.ExecuteFunction("GetHotelImage(@p_id)", id);
            return result.Rows[0][0].ToString();
        }

        /// <summary>
        ///     Metodo para formar un <see cref="Hotel" /> a partir de un <see cref="DataRow" />.
        ///     El <see cref="DataRow" /> debe cumplir con un orden especifico de parametros para funcionar,
        ///     el cual es respetado por todos las funciones almacenadas relacionadas al modulo de Hotel.
        /// </summary>
        /// <param name="row">
        ///     Fila en donde se encuentran los datos para generar el <see cref="Hotel" />
        /// </param>
        /// <returns><see cref="Hotel" /> segun los datos recibidos</returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     Ejecutado si el DataRow no devuelve la cantidad de atributos necesarios
        /// </exception>
        /// <exception cref="FormatException">
        ///     Ejecutado si algun elemento del DataRow no esta en el orden correcto y por lo tanto no se
        ///     puede convertir al tipo de dato correspondiente para el <see cref="Hotel" />
        /// </exception>
        private static Hotel ExtractHotelFromRow(DataRow row)
        {
            var id = Convert.ToInt32(row[0]);
            var name = row[1].ToString();
            var amountOfRooms = Convert.ToInt32(row[2]);
            var roomCapacity = Convert.ToInt32(row[3]);
            var isActive = Convert.ToBoolean(row[4]);
            var addressSpecs = row[5].ToString();
            var pricePerRoom = Convert.ToDecimal(row[6]);
            var website = row[7].ToString();
            var phone = row[8].ToString();
            var picture = row[9].ToString();
            var stars = Convert.ToInt32(row[10]);
            var locationId = Convert.ToInt32(row[11]);

            return HotelBuilder.Create()
                .IdentifiedBy(id)
                .WithName(name)
                .WithAmountOfRooms(amountOfRooms)
                .WithCapacityPerRoom(roomCapacity)
                .WithPricePerRoom(pricePerRoom)
                .WithPhone(phone)
                .WithWebsite(website)
                .WithStars(stars)
                .WithPictureUrl(picture)
                .LocatedAt((locationId))
                .WithStatus(isActive)
                .WithAddressDescription(addressSpecs)
                .Build();
        }
    }
}