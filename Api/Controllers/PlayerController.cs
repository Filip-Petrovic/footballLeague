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
    public class PlayerController : ControllerBase
    {
        private IAddPlayerCommand _addPlayerCommand;
        private IEditPlayerCommand _editPlayerCommand;
        private IDeletePlayerCommand _deletePlayerCommand;
        private IGetPlayerCommand _getPlayerCommand;
        private IGetPlayersCommand _getPlayersCommand;

        public PlayerController(IAddPlayerCommand addPlayerCommand, IEditPlayerCommand editPlayerCommand, IDeletePlayerCommand deletePlayerCommand, IGetPlayerCommand getPlayerCommand, IGetPlayersCommand getPlayersCommand)
        {
            _addPlayerCommand = addPlayerCommand;
            _editPlayerCommand = editPlayerCommand;
            _deletePlayerCommand = deletePlayerCommand;
            _getPlayerCommand = getPlayerCommand;
            _getPlayersCommand = getPlayersCommand;
        }

        // GET: api/Player
        /// <summary>
        /// Dohvata igrace uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "Email": "email@gmail.com",
        ///        "CityId": 1,
        ///        "ClubId": 2,
        ///        "PositionId": 3
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene igrace</response>
        /// <response code="500">Greska na serveru</response>
        [HttpGet]
        public ActionResult<IEnumerable<PlayerDto>> Get([FromQuery] PlayerSearch playerSearch)
        {
            try
            {
                var players = _getPlayersCommand.Execute(playerSearch);
                return Ok(players);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Player/5
        /// <summary>
        /// Dohvata igraca po id-u.
        /// </summary>
        /// <response code="200">Vraca trazenog igraca</response>
        /// <response code="404">Ako ne postoji igrac sa tim id-om</response> 
        /// <response code="500">Serverska greska</response>
        [HttpGet("{id}")]
        public ActionResult<PlayerDto> Get(int id)
        {
            try
            {
                var player = _getPlayerCommand.Execute(id);
                return Ok(player);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Player
        /// <summary>
        /// Dodavanje novog igraca
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "Email": "email@gmail.com",
        ///        "CityId": 1,
        ///        "ClubId": 2,
        ///        "PositionId": 3
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novog igraca</response>
        /// <response code="500">Serverska greska</response>
        [HttpPost]
        public IActionResult Post([FromBody] PlayerDto playerDto)
        {
            try
            {
                _addPlayerCommand.Execute(playerDto);
                return StatusCode(201);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Player/5
        /// <summary>
        /// Izmena postojeceg igraca
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    PUT
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "Email": "email@gmail.com",
        ///        "CityId": 1,
        ///        "ClubId": 2,
        ///        "PositionId":3
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Izmena Igraca</response>
        /// <response code="404">Igrac sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayerDto playerDto)
        {
            try
            {
                playerDto.Id = id;
                _editPlayerCommand.Execute(playerDto);
                return NoContent();
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Brise igraca
        /// </summary>
        /// <response code="201">Brise igraca</response>
        /// <response code="404">igrac sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deletePlayerCommand.Execute(id);
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
