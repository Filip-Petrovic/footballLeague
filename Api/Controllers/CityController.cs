using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private IGetCityCommand _getCityCommand;
        private IGetCitiesCommand _getCitiesCommand;
        private IAddCityCommand _addCityCommand;
        private IEditCityCommand _editCityCommand;
        private IDeleteCityCommand _deleteCityCommand;

        public CityController(IGetCityCommand getCityCommand, IGetCitiesCommand getCitiesCommand, IAddCityCommand addCityCommand, IEditCityCommand editCityCommand, IDeleteCityCommand deleteCityCommand)
        {
            _getCityCommand = getCityCommand;
            _getCitiesCommand = getCitiesCommand;
            _addCityCommand = addCityCommand;
            _editCityCommand = editCityCommand;
            _deleteCityCommand = deleteCityCommand;
        }

        // GET: api/City
        /// <summary>
        /// Dohvata sve gradove, uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "Paracin",
        ///        "PostalCode": 35250
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene gradove</response>
        /// <response code="500">Serverska greska</response> 
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> Get([FromQuery] CitySearch citySearch)
        {
            try
            {
                var cities = _getCitiesCommand.Execute(citySearch);
                return Ok(cities);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/City/5

        /// <summary>
        /// Dohvata grad po id-u.
        /// </summary>
        /// <response code="200">Vraca trazeni grad</response>
        /// <response code="404">Ako ne postoji grad sa tim id-om</response> 
        /// <response code="500">Serverska greska</response>
        [HttpGet("{id}")]
        public ActionResult<CityDto> Get(int id)
        {
            try
            {
                var city = _getCityCommand.Execute(id);
                return Ok(city);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/City
        /// <summary>
        /// Dodavanje novog grada
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        
        ///        "Name": "Paracin",
        ///        "PostalCode": 35250
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novi grad</response>
        /// <response code="409">Grad sa tim imenom vec postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPost]
        public IActionResult Post([FromBody] CityDto value)
        {
            try
            {
                _addCityCommand.Execute(value);
                return StatusCode(201);
            }
            catch (EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/City/5
        /// <summary>
        /// Izmena grada
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///     {
        ///        "Name": "Paracin",
        ///        "PostalCode": 35250
        ///     }
        ///
        /// </remarks>
        /// <response code="201">menja grad</response>
        /// <response code="404">grad sa tim id-om nepostoji</response>
        /// <response code="409"> grad sa tim imenom ili postalCode-om vec postoji </response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CityDto value)
        {
            value.Id = id;
            try
            {
                _editCityCommand.Execute(value);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Brise grad
        /// </summary>
        /// <response code="201">Brise grad</response>
        /// <response code="404">grad sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCityCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
