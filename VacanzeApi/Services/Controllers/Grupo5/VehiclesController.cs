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


namespace vacanze_back.VacanzeApi.Services.Controllers.Grupo5
{
    [Produces ("application/json")]
    [Route ("api/[controller]")]
    [EnableCors ("MyPolicy")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        [Consumes ("application/json")]
        [HttpPost]
        public IActionResult AddVehicle ([FromBody] VehicleDTO vehicleDTO) {
            try {
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                Entity entity = vehicleMapper.CreateEntity(vehicleDTO);
                AddVehicleCommand command = CommandFactory.CreateAddVehicleCommand((Vehicle) entity);
                command.Execute();
                return Ok ("ok " + command.GetResult ());
            }catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet("{vehicleId:int}")]
        public ActionResult<VehicleDTO> GetVehicleById(int vehicleId){
            try {
                GetVehicleByIdCommand command = CommandFactory.CreateGetVehicleByIdCommand(vehicleId);
                command.Execute ();
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                return Ok (vehicleMapper.CreateDTO(command.GetResult()));
            } catch (VehicleNotFoundException ex){
                return StatusCode (404, ex.Message + ex.VehicleId);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }

        [HttpGet("~/api/locations/{locationId:int}/[controller]")]
        public ActionResult<List<VehicleDTO>> GetAvailableVehiclesByLocation(int locationId){
            try {
                GetAvailableVehiclesByLocationCommand command = 
                    CommandFactory.CreateGetAvailableVehiclesByLocationCommand(locationId);
                command.Execute ();
                VehicleMapper vehicleMapper = MapperFactory.CreateVehicleMapper();
                return Ok (vehicleMapper.CreateDTOList(command.GetResult()));
            } catch (LocationNotFoundException ex){
                return StatusCode (404, "Location con  " + ex.Message);
            } catch (NotVehiclesAvailableException ex){
                return StatusCode (404, ex.Message);
            } catch (VehicleNotFoundException ex){
                return StatusCode (404, ex.Message + ex.VehicleId);
            } catch (InternalServerErrorException ex) {
                return StatusCode (500, ex.Message);
            } catch (Exception) {
                return StatusCode (400);
            }
        }
    }
}