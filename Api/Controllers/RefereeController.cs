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
    public class RefereeController : ControllerBase
    {
        // GET: api/Referee

        private IAddRefereeCommand _addReferee;
        private IGetRefereeCommand _getReferee;
        private IGetRefereesCommand _getReferees;
        private IDeleteRefereeCommand _deleteReferees;
        private IEditRefereeCommand _editReferee;

        public RefereeController(IAddRefereeCommand addReferee, IGetRefereeCommand getReferee, IGetRefereesCommand getReferees, IDeleteRefereeCommand deleteReferees, IEditRefereeCommand editReferee)
        {
            _addReferee = addReferee;
            _getReferee = getReferee;
            _getReferees = getReferees;
            _deleteReferees = deleteReferees;
            _editReferee = editReferee;
        }

        /// <summary>
        /// Dohvata sve sudije, uz mogucnost pretrage
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Vraca trazene sudije</response>
        /// <response code="500">Serverska greska</response> 
        [HttpGet]
        public ActionResult<IEnumerable<RefereeDto>> Get([FromQuery] RefereeSearch refereeSearch)
        {
            try
            {
                var referees = _getReferees.Execute(refereeSearch);
                return Ok(referees);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        // GET: api/Referee/5
        /// <summary>
        /// Dohvata sudiju po id-u.
        /// </summary>
        /// <response code="200">Vraca trazenog sudiju</response>
        /// <response code="404">Ako ne postoji sudija sa tim id-om</response> 
        /// <response code="500">Serverska greska</response>
        [HttpGet("{id}")]
        public ActionResult<RefereeDto> Get(int id)
        {
            try
            {
                RefereeDto referee = _getReferee.Execute(id);

                return Ok(referee);
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

        // POST: api/Referee
        /// <summary>
        /// Dodavanje novog sudije
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "LeaguesId": [1,2,3]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Dodaje novu ligu</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPost]
        public IActionResult Post([FromBody] RefereeDto refDto)
        {
            try
            {
                _addReferee.Execute(refDto);
                return StatusCode(201, "Referee has been successfully added");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Referee/5
        /// <summary>
        /// Izmena postojeceg sudije
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "FirstName": "Filip",
        ///        "LastName": "Petrovic",
        ///        "LeaguesId": [1,2,3]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Izmena sudije</response>
        /// <response code="404">Sudija sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RefereeDto refDto)
        {
            try
            {
                refDto.Id = id;
                _editReferee.Execute(refDto);
                return NoContent();
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

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Brise sudiju
        /// </summary>
        /// <response code="201">Brise sudiju</response>
        /// <response code="404">Sudija sa tim id-om ne postoji</response>
        /// <response code="500">Serverska greska</response> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteReferees.Execute(id);
                return NoContent();
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
    }
}
