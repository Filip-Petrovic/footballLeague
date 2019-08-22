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
    public class PositionController : ControllerBase
    {

        private IGetPositionCommand _getPosition;
        private IGetPositionsCommand _getPositions;
        private IEditPositionCommand _editPosition;
        private IDeletePositionCommand _deletePosition;
        private IAddPositionCommand _addPosition;

        public PositionController(IGetPositionCommand getPosition, IGetPositionsCommand getPositions, IEditPositionCommand editPosition, IDeletePositionCommand deletePosition, IAddPositionCommand addPosition)
        {
            _getPosition = getPosition;
            _getPositions = getPositions;
            _editPosition = editPosition;
            _deletePosition = deletePosition;
            _addPosition = addPosition;
        }





        // GET: api/Position
        /// <summary>
        /// Dohvata sve pozicije, uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "Sredina"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene pozicije</response>
        /// <response code="500">Serverska greska</response> 
        [HttpGet]
        public ActionResult<IEnumerable<PositionDto>> Get([FromQuery] PositionSearch positionSearch)
        {
            try
            {
                var positions = _getPositions.Execute(positionSearch);
                return Ok(positions);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }

        }

        // GET: api/Position/5
        /// <summary>
        /// Dohvata jednu poziciju
        /// </summary>
        /// <response code="200">Vraca trazenu poziciju</response>
        /// <response code="404">Trazena pozicija ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpGet("{id}")]
        public ActionResult<PositionDto> Get(int id)
        {
            try
            {
                var position = _getPosition.Execute(id);
                return Ok(position);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured.");
            }
        }

        // POST: api/Position
        /// <summary>
        /// Dodavanje nove pozicije
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name":"Napadac"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novu poziciju</response>
        /// <response code="409">Pozicija vec postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPost]
        public IActionResult Post([FromBody] PositionDto positionDto)
        {
            try
            {
                _addPosition.Execute(positionDto);
                
                return StatusCode(201, "Position has been successfully added");
            }
            catch(EntityAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Position/5
        /// <summary>
        /// Menja postojecu poziciju
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name":"odbrana"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Menja poziciju</response>
        /// <response code="404">Pozicija sa tim id-om ne postoji</response>
        /// <response code="409">Pozicija vec postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PositionDto positionDto)
        {
            try
            {
                positionDto.Id = id;
                _editPosition.Execute(positionDto);
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
        /// Brise poziciju
        /// </summary>
        /// <response code="201">Brise poziciju</response>
        /// <response code="404">Pozicija ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deletePosition.Execute(id);
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
