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
    public class ClubController : ControllerBase
    {

        private IGetClubCommand _getClubCommand;
        private IGetClubsCommand _getClubsCommand;
        private IAddClubCommand _addClubCommand;
        private IEditClubCommand _editClubCommand;
        private IDeleteClubCommand _deleteClubCommand;

        public ClubController(IGetClubCommand getClubCommand, IGetClubsCommand getClubsCommand, IAddClubCommand addClubCommand, IEditClubCommand editClubCommand, IDeleteClubCommand deleteClubCommand)
        {
            _getClubCommand = getClubCommand;
            _getClubsCommand = getClubsCommand;
            _addClubCommand = addClubCommand;
            _editClubCommand = editClubCommand;
            _deleteClubCommand = deleteClubCommand;
        }

        // GET: api/Club
        /// <summary>
        /// Dohvata sve klubove, uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "Jedinstvo",
        ///        "CityId": 2,
        ///        "LeagueId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene klubove</response>
        /// <response code="500">Serverska greska</response>
        [HttpGet]
        public ActionResult<IEnumerable<ClubDto>> Get([FromQuery] ClubSearch clubSearch)
        {
            try
            {
                var clubs = _getClubsCommand.Execute(clubSearch);
                return Ok(clubs);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Club/5
        /// <summary>
        /// Dohvata klub po id-u.
        /// </summary>
        /// <response code="200">Vraca trazeni klub</response>
        /// <response code="404">Ako ne postoji klub sa tim id-om</response> 
        /// <response code="500">Serverska greska</response>
        [HttpGet("{id}")]
        public ActionResult<ClubDto> Get(int id)
        {

            try
            {
                var club = _getClubCommand.Execute(id);
                return Ok(club);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Club

        /// <summary>
        /// Dodavanje novog kluba
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "Name": "Jedinstvo",
        ///        "CityId": 2,
        ///        "LeagueId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novog kluba</response>
        /// <response code="404">Grad ili liga ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPost]
        public IActionResult Post([FromBody] ClubDto value)
        {
            try
            {
                _addClubCommand.Execute(value);
                return StatusCode(201);
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

        // PUT: api/Club/5
        /// <summary>
        /// Izmena kluba
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///     {
        ///        "Name": "Jedinstvo",
        ///        "CityId": 2,
        ///        "LeagueId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novu ligu</response>
        /// <response code="404">klub sa tim id-om nepostoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ClubDto value)
        {
            value.Id = id;
            try
            {
                _editClubCommand.Execute(value);
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

        // DELETE: api/ApiWithActions/5

        /// <summary>
        /// Brise club
        /// </summary>
        /// <response code="201">Brise club</response>
        /// <response code="404">club sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteClubCommand.Execute(id);
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
