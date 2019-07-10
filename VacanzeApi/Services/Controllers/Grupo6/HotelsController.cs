using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo6;
using vacanze_back.VacanzeApi.Common.Entities.Grupo8;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo8;
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
		 private readonly ILogger<HotelsController> _logger;

        public HotelsController (ILogger<HotelsController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        ///     Metodo para obtener los hoteles por ubicacion o sin ella.
        /// </summary>
        /// <param name="location">Id de la ubicacion donde se encuentran los hoteles.</param>
        /// <returns>Objeto tipo json de los hoteles</returns>
        [HttpGet]
        public ActionResult<IEnumerable<HotelDTO>> Get([FromQuery] int location = -1)
        {
			GetHotelsCommand commandHotels = CommandFactory.GetHotelsCommand();
			GetHotelsByCityCommand commandByCity = CommandFactory.GetHotelsByCityCommand(location);
            commandHotels.Execute();
            commandByCity.Execute();
            var resulthotel = commandHotels.GetResult();
            _logger?.LogInformation($"Se obtuvieron los hoteles exitosamente");
            var resultcity=commandByCity.GetResult();
            _logger?.LogInformation($"Obtenida las ciudades exitosamente por: {location}");  
                HotelMapper hotelMapper = MapperFactory.createHotelMapper(); 
            return location == -1
                ?  hotelMapper.CreateDTOList(resulthotel)
                :  hotelMapper.CreateDTOList(resultcity);
        }
        /// <summary>
        ///     Metodo para buscar los hoteles por Id
        /// </summary>
        /// <param name="hotelId">ID del hotel a buscar</param>
        /// <returns>Objeto tipo json del hotel encontrado</returns>
        /// <exception cref="HotelNotFoundException">Lanzada si no existe el hotel</exception>
        [HttpGet("{hotelId}", Name = "GetHotelById")]
        public ActionResult<HotelDTO> GetById([FromRoute] int hotelId)
        {
            try
            {
                HotelMapper hotelMapper = MapperFactory.createHotelMapper();
                GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(hotelId);
                commandId.Execute ();
                var result = commandId.GetResult();
                _logger?.LogInformation($"Obtenido el hotel exitosamente por Id = {hotelId}");
                DTO lDTO  = hotelMapper.CreateDTO(result);               
                return Ok(lDTO);
            }
            catch (HotelNotFoundException ex)
            {
				_logger?.LogWarning( $"Hotel con Id = {hotelId} no conseguido al consultar");
                return NotFound();
            }
        }
        /// <summary>
        ///     Metodo para crear un hotel
        /// </summary>
        /// <param name="hotelDTo">Objeto Hotel a crear</param>
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
                HotelMapper hotelMapper = MapperFactory.createHotelMapper();
                Entity entity = hotelMapper.CreateEntity(hotelDTO);
                AddHotelCommand command = CommandFactory.createAddHotelCommand ((Hotel)entity);
                command.Execute ();
                int idFromData = command.GetResult(); 
                _logger?.LogInformation($"Obtenido el id = {idFromData} el Hotel exitosamente al agregar");
                GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(idFromData);
                commandId.Execute ();
                var result = commandId.GetResult();
                _logger?.LogInformation($"Obtenido el hotel exitosamente por Id = {idFromData}");
                DTO lDTO  = hotelMapper.CreateDTO( result);               
                    return CreatedAtAction ("Get", "hotels", lDTO);
            }
            catch (RequiredAttributeException e)
            {
				_logger?.LogWarning($"Atributo requerido no recibido al agregar Hotel: {e.Message}");
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
				_logger?.LogWarning($"Valor del atributo es invalido al agregar Hotel: {e.Message}");
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        ///     Metodo para eliminar un hotel
        /// </summary>
        /// <param name="id">ID del hotel a eliminar</param>
        /// <returns>Una respuesta 200 vacia</returns>
        /// <exception cref="HotelNotFoundException">El hotel a eliminar no existe</exception>
        [HttpDelete("{id}", Name = "DeleteHotel")]
        public ActionResult Delete([FromRoute] int id){
        try 
        {    //validar que el hotel exista
            GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(id);
            commandId.Execute ();
            
            DeleteHotelCommand command = CommandFactory.DeleteHotelCommand(id);
            command.Execute();
            return Ok();
        }
        catch (HotelNotFoundException ex)
            {
                _logger?.LogWarning( $"Hotel con id = {id} no conseguido al intentar eliminar");
                return new NotFoundObjectResult(new ErrorMessage($"Hotel con id = {id} no conseguido"));
            }
        }
        /// <summary>
        ///     Metodo para modifcar un hotel
        /// </summary>
        /// <param name="hotelId">ID del hotel a modificar</param>
        /// <param name="HotelDTo">Objeto hotel con la data para el hotel a modificar</param>
        /// <returns>Objeto tipo json del hotel modificado</returns>
        /// <exception cref="HotelNotFoundException">El hotel a modificar no existe</exception>
        /// <exception cref="RequiredAttributeException">Algun atributo requerido estaba como null</exception>
        /// <exception cref="InvalidAttributeException">Algun atributo tenia un valor invalido</exception>
        [HttpPut("{hotelId}", Name = "UpdateHotel")]
        public ActionResult<HotelDTO> Update([FromRoute] int hotelId, [FromBody] HotelDTO hotelDTO)
        {
            try
            {
				GetHotelByIdCommand commandId =  CommandFactory.GetHotelByIdCommand(hotelId);
                commandId.Execute ();
				HotelMapper hotelMapper = MapperFactory.createHotelMapper();

                Entity entity = hotelMapper.CreateEntity(hotelDTO);
                UpdateHotelCommand command = CommandFactory.UpdateHotelCommand(hotelId, (Hotel) entity);
                command.Execute ();
                var result = command.GetResult();
                _logger?.LogInformation($"Obtenido el hotel exitosamente, despues de actualizar con el Id = {hotelId}");
                DTO lDTO  = hotelMapper.CreateDTO( result);               
                return Ok(lDTO);
            }
            catch (HotelNotFoundException ex)
            {
                _logger?.LogWarning( "Hotel con id = {hotelId} no conseguido al intentar actualizar");
                return new NotFoundObjectResult(new ErrorMessage($"Hotel con id {hotelId} no conseguido"));
            }
            catch (RequiredAttributeException e)
            {
				_logger?.LogWarning( $"El atributo requerido no fue recibido al actualizar el Hotel: {e.Message}");
                return new BadRequestObjectResult(new ErrorMessage(e.Message));
            }
            catch (InvalidAttributeException e)
            {
				_logger?.LogWarning($"El valor del atributo es invalido al actualizar el Hotel: {e.Message}");
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
			GetHotelImageCommand commandImage =  CommandFactory.GetHotelImageCommand(hotelId);
            commandImage.Execute();
            var result = commandImage.GetResult();
            _logger?.LogInformation($"Se obtuvo la imagen del hotel exitosamente con el Id: {hotelId}");
            return result;
        }
    }
}