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
    public class LeagueController : ControllerBase
    {

        private IGetLeagueCommand _getLeagueCommand;
        private IGetLeaguesCommand _getLeaguesCommand;
        private IAddLeagueCommand _addLeagueCommand;
        private IEditLeagueCommand _editLeagueCommand;
        private IDeleteLeagueCommand _deleteLeagueCommand;

        public LeagueController(IGetLeagueCommand getLeagueCommand, IGetLeaguesCommand getLeaguesCommand, IAddLeagueCommand addLeagueCommand, IEditLeagueCommand editLeagueCommand, IDeleteLeagueCommand deleteLeagueCommand)
        {
            _getLeagueCommand = getLeagueCommand;
            _getLeaguesCommand = getLeaguesCommand;
            _addLeagueCommand = addLeagueCommand;
            _editLeagueCommand = editLeagueCommand;
            _deleteLeagueCommand = deleteLeagueCommand;
        }


        // GET: api/League
        /// <summary>
        /// Dohvata sve lige, uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "Jelen Super liga",
        ///        "Lever": "1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene lige</response>
        /// <response code="500">Serverska greska</response> 
        [HttpGet]
        public ActionResult<IEnumerable<LeagueDto>> Get([FromQuery] LeagueSearch leagueSearch)
        {
            try
            {
                var leagues = _getLeaguesCommand.Execute(leagueSearch);
                return Ok(leagues);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/League/5
        /// <summary>
        /// Dohvata ligu po id-u.
        /// </summary>
        /// <response code="200">Vraca trazenu ligu</response>
        /// <response code="404">Ako ne postoji liga sa tim id-om</response> 
        /// <response code="500">Serverska greska</response>
        [HttpGet("{id}")]
        public ActionResult<LeagueDto> Get(int id)
        {
            try
            {
                var league = _getLeagueCommand.Execute(id);
                return Ok(league);
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

        // POST: api/League
        /// <summary>
        /// Dodavanje nove lige
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "RefereesId": [1,2,5]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novu ligu</response>
        /// <response code="404">Sudija ne postoji</response>
        /// <response code="409">Liga sa tim imenom vec postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPost]
        public IActionResult Post([FromBody] LeagueDto leagueDto)
        {
            try
            {
                _addLeagueCommand.Execute(leagueDto);
                return StatusCode(201);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
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

        // PUT: api/League/5

        /// <summary>
        /// Izmenalige
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT 
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novu ligu</response>
        /// <response code="409">Liga sa tim imenom vec postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LeagueDto leagueDto)
        {
            leagueDto.Id = id;
            try
            {
                _editLeagueCommand.Execute(leagueDto);
                return NoContent();
            }

            catch (EntityNotFoundException e)
            {
                return Conflict(e.Message);
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

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Brise ligu
        /// </summary>
        /// <response code="201">Brise Ligu</response>
        /// <response code="404">Liga sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteLeagueCommand.Execute(id);
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
