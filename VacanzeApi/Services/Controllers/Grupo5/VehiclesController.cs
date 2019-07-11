using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vacanze_back.VacanzeApi.Common.Entities;
using vacanze_back.VacanzeApi.Common.Entities.Grupo5;
using vacanze_back.VacanzeApi.Common.Exceptions;
using vacanze_back.VacanzeApi.Common.Exceptions.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Command;
using vacanze_back.VacanzeApi.LogicLayer.Command.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.DTO.Grupo5;
using vacanze_back.VacanzeApi.LogicLayer.Mapper;
using vacanze_back.VacanzeApi.LogicLayer.Mapper.Grupo5;
using vacanze_back.VacanzeApi.Persistence.Repository.Grupo5;
using Microsoft.Extensions.Logging;


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5
{
    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {   
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ILogger<VehiclesController> logger)
        {
            _logger = logger;
        }

        ///<sumary>Creación de un nuevo vehiculo</sumary>
        ///<param name="vehicleDTO">DTO de vehiculo</param>
        ///<returns>Success: id del vehiculo</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddVehicle ([FromBody] VehicleDTO vehicleDTO) {
            _logger?.LogInformation($"Inicio del servicio: [Post] https://localhost:5001/api/vehicles ");
            try {
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                Entity entity = vehicleMapper.CreateEntity(vehicleDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                AddVehicleCommand command = CommandFactory.CreateAddVehicleCommand((Vehicle) entity);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute();
                return Ok ("ok " + command.GetResult ());
            } catch (RequiredAttributeException ex){
                _logger?.LogWarning("Atributo requerido: " +    ex.Message);
                return StatusCode(400, ex.Message);
            } catch (ModelNotFoundException ex){
                _logger?.LogWarning("Modelo con Id : " + ex.ModelId + "no encontrado");
                return StatusCode(404, ex.Message + ex.ModelId);
            } catch (LocationNotFoundException ex){
                _logger?.LogWarning("Lugar no encontrado");
                return StatusCode(404, ex.Message);
            } catch (UniqueAttributeException ex){
                _logger?.LogWarning(ex.Message);
                return StatusCode(500, ex.Message); 
            }catch (InternalServerErrorException ex) {
                 _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener vehiculo por su Id</sumary>
        ///<param name="vehicleId">Id del vehiculo</param>
        ///<returns>Success: VehicleDTO</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet("{vehicleId:int}")]
        public ActionResult<VehicleDTO> GetVehicleById(int vehicleId){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/vehicles/vehicleId ");
            try {
                GetVehicleByIdCommand command = CommandFactory.CreateGetVehicleByIdCommand(vehicleId);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                return Ok (vehicleMapper.CreateDTO(command.GetResult()));
            } catch (VehicleNotFoundException ex){
                _logger?.LogWarning("Vehiculo con Id : " + ex.VehicleId + "no encontrado");
                return StatusCode (404, ex.Message + ex.VehicleId);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener vehiculos disponibles por ciudad</sumary>
        ///<param name="locationId">Id de la ciudad</param>
        ///<returns>Success: Lista de Vehicles</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet("~/api/locations/{locationId:int}/[controller]")]
        public ActionResult<List<Vehicle>> GetAvailableVehiclesByLocation(int locationId){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/locations/locationId/vehicles ");
            try {
                GetAvailableVehiclesByLocationCommand command = 
                    CommandFactory.CreateGetAvailableVehiclesByLocationCommand(locationId);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                return Ok (command.GetResult());
            } catch (LocationNotFoundException ex){
                _logger?.LogWarning("Lugar no encontrado");
                return StatusCode (404, "Location con  " + ex.Message);
            } catch (NotVehiclesAvailableException ex){
                _logger?.LogWarning(ex.Message);
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Obtener todos los vehiculos</sumary>
        ///<returns>Success: Lista de Vehicles</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpGet]
        public ActionResult<List<Vehicle>> GetVehicles(){
            _logger?.LogInformation($"Inicio del servicio: [GET] https://localhost:5001/api/vehicles ");
             try {
                GetVehiclesCommand command = 
                    CommandFactory.CreateGetVehiclesCommand();
                    _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                return Ok (command.GetResult());
            }  catch (NotVehiclesAvailableException ex){
                 _logger?.LogWarning(ex.Message);
                return StatusCode (404, ex.Message);
            } catch (InternalServerErrorException ex) {
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                _logger?.LogError("Error inesperado");
                return StatusCode (400);
            }
        }

        ///<sumary>Actualizar un vehiculo</sumary>
        ///<param name="vehicleDTO">DTO de vehiculo</param>
        ///<returns>Success: mensaje de ok</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpPut]
        public IActionResult UpdateVehicle([FromBody] VehicleDTO vehicleDTO){
            _logger?.LogInformation($"Inicio del servicio: [PUT] https://localhost:5001/api/vehicles ");
            try{
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                Entity entity = vehicleMapper.CreateEntity(vehicleDTO);
                _logger?.LogInformation($" Se transformó de DTO a Entity ");
                UpdateVehicleCommand command = CommandFactory.CreateUpdateVehicleCommand ((Vehicle) entity);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch (VehicleNotFoundException ex){
                _logger?.LogWarning("Vehiculo con Id : " + ex.VehicleId + "no encontrado");
                return StatusCode(404, ex.Message + ex.VehicleId);
            } catch (ModelNotFoundException ex){
                _logger?.LogWarning("Modelo con Id : " + ex.ModelId + "no encontrado");
                return StatusCode(404, ex.Message + ex.ModelId);
            } catch (LocationNotFoundException ex){
                _logger?.LogWarning("Lugar no encontrado");
                return StatusCode(404, ex.Message);
            }  catch(InternalServerErrorException ex){
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode(500, ex.Message);
            } catch(Exception){
                _logger?.LogError("Error inesperado");
                return StatusCode(400);
            }
        }
        
        ///<sumary>Actualizar estatus de unvehiculo</sumary>
        ///<param name="vehicleId">Id del vehiculo</param>
        ///<param name="status">boolean de status</param>
        ///<returns>Success: mensaje de ok</returns>
        ///<returns>No Success: Mensaje de error</returns>
        [HttpPut("{vehicleId:int}/update/")]
        public IActionResult UpdateVehicleStatus(int vehicleId, [FromQuery] bool status){
            _logger?.LogInformation($"Inicio del servicio: [PUT] https://localhost:5001/api/vehicles/vehicleId/update?status='' ");
            try{
                UpdateVehicleStatusCommand command = 
                    CommandFactory.CreateUpdateVehicleStatusCommand (vehicleId, status);
                _logger?.LogInformation($" Ejecución del comando ");
                command.Execute ();
                if(command.GetResult())
                    return Ok("La modificación fue realizada exitosamente");
                else
                    return StatusCode(400);
            } catch(VehicleNotFoundException ex){
                 _logger?.LogWarning("Vehiculo con Id : " + ex.VehicleId + "no encontrado");
                return StatusCode(404, ex.Message + ex.VehicleId);
            } catch(InternalServerErrorException ex){
                _logger?.LogError("Error: " + ex.Ex.Message);
                return StatusCode(500, ex.Message);
            } catch(Exception){
                _logger?.LogError("Error inesperado");
                return StatusCode(400);
            }
        }

    }
}