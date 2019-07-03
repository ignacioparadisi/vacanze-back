using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6; //con el tiempo debemos quitar la entidad por a fabricaEntiti
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;  //dcon el tiempo debemos quitar la entitad por a fabricaentiti
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo6; //con el tiempo debemos quitar esta de repository
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.DTO;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo6;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo6;

namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo6
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        /// <summary>
        ///     Metodo para obtener los hoteles por ubicacion o sin ella
        /// </summary>
        /// <param name="location">id de la ubicacion donde se encuentran los hoteles</param>
        /// <returns>Objeto tipo json de los hoteles</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] int location = -1)
        {
            return location == -1
                ? HotelRepository.GetHotels()
                : HotelRepository.GetHotelsByCity(location);
        }

        /// <summary>
        ///     Metodo para buscar los hoteles por id
        /// </summary>
        /// <param name="hotelId">ID del hotel a buscar</param>
        /// <returns>Objeto tipo json del hotel encontrado</returns>
        /// <exception cref="HotelNotFoundException">Lanzada si no existe el hotel</exception>
        [HttpGet("{hotelId}", Name = "GetHotelById")]
        public ActionResult<HotelDTO> GetById([FromRoute] int hotelId)
        {
            try
            {
                Console.WriteLine("aquiii controlgret");
                HotelMapper HotelMapper = MapperFactory.createHotelMapper();
                GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(hotelId);
                commandId.Execute ();
                DTO lDTO  = HotelMapper.CreateDTO((Hotel) commandId.GetResult());               
                return Ok(lDTO);
                
                //Metodo original 
                //return Ok(HotelRepository.GetHotelById(hotelId));
            }
            catch (HotelNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Metodo para crear un hotel
        /// </summary>
        /// <param name="hotel">Objeto Hotel a crear</param>
        /// <returns>Objeto tipo json del hotel creado</returns>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HotelDTO> Create([FromBody] HotelDTO hotelDTO)
        {
        
            try
            {     
                Console.WriteLine("aquiii control add 1");
                // Metodo Original 
                //cambiar el [FromBody] HotelDTO hotelDTO por [FromBody] Hotel hotel
                /*var idFromDatabase = HotelRepository.AddHotel((Hotel) entity);
                return CreatedAtAction("Get", "hotels",
                    HotelRepository.GetHotelById(idFromDatabase)); */

                 // herick probando patrones 
                HotelMapper HotelMapper = MapperFactory.createHotelMapper();
                Entity entity = HotelMapper.CreateEntity(hotelDTO);
                AddHotelCommand command = CommandFactory.createAddHotelCommand ((Hotel) entity);
                command.Execute ();
                int idFromData = command.GetResult(); 
 
                GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(idFromData);
                commandId.Execute ();
                DTO lDTO  = HotelMapper.CreateDTO((Hotel) commandId.GetResult());               
                    return CreatedAtAction ("Get", "hotels", lDTO);
            }
            catch (RequiredAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Metodo para eliminar un hotel
        /// </summary>
        /// <param name="id">ID del hotel a eliminar</param>
        /// <returns>una respuesta 200 vacia</returns>
        [HttpDelete("{id}", Name = "DeleteHotel")]
        public ActionResult Delete([FromRoute] int id)
        {
             DeleteHotelCommand command = CommandFactory.DeleteHotelCommand (id);
             command.Execute ();
            return Ok();
        }

        /// <summary>
        ///     Metodo para modifcar un hotel
        /// </summary>
        /// <param name="hotelId">ID del hotel a modificar</param>
        /// <param name="dataToUpdate">Objeto hotel con la data para el hotel a modificar</param>
        /// <returns>Objeto tipo json del hotel modificado</returns>
        /// <exception cref="HotelNotFoundException">El hotel a modifcar no existe</exception>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        [HttpPut("{hotelId}", Name = "UpdateHotel")]
        public ActionResult<Hotel> Update([FromRoute] int hotelId, [FromBody] HotelDTO hotelDTO)
        {
            try
            {
				GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(hotelId);
                commandId.Execute ();
               // DTO lDTO  = HotelMapper.CreateDTO((Hotel) commandId.GetResult());  

                //var updated = HotelRepository.UpdateHotel(hotelId, dataToUpdate);
				HotelMapper HotelMapper = MapperFactory.createHotelMapper();
                Entity entity = HotelMapper.CreateEntity(hotelDTO);
                UpdateHotelCommand command = CommandFactory.UpdateHotelCommand(hotelId, (Hotel) entity);
                command.Execute ();
                DTO lDTO  = HotelMapper.CreateDTO((Hotel) command.GetResult());               
                return Ok(lDTO);
            }
            catch (HotelNotFoundException)
            {
                return new NotFoundObjectResult(
                    new ErrorMessage($"Hotel con id {hotelId} no conseguido"));
            }
            catch (RequiredAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Metodo para obtener la imagen de un hotel
        /// </summary>
        /// <param name="hotelId">ID del hotel que posee la imagen</param>
        /// <returns>Un string base64 de la imagen del hotel</returns>
        [HttpGet("{hotelId}/image")]
        public string GetHotelImage([FromRoute] int hotelId)
        {
            return HotelRepository.GetHotelImage(hotelId);
        }
    }
}