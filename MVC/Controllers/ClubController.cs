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

namespace MVC.Controllers
{
    public class ClubController : Controller
    {

        private IGetClubCommand _getClubCommand;
        private IGetClubsCommand _getClubsCommand;
        private IAddClubCommand _addClubCommand;
        private IEditClubCommand _editClubCommand;
        private IDeleteClubCommand _deleteClubCommand;
        private IGetCitiesCommand _getCitiesCommand;
        private IGetLeaguesCommand _getLeaguesCommand;

        public ClubController(IGetClubCommand getClubCommand, IGetClubsCommand getClubsCommand, IAddClubCommand addClubCommand, IEditClubCommand editClubCommand, IDeleteClubCommand deleteClubCommand, IGetCitiesCommand getCitiesCommand, IGetLeaguesCommand getLeaguesCommand)
        {
            _getClubCommand = getClubCommand;
            _getClubsCommand = getClubsCommand;
            _addClubCommand = addClubCommand;
            _editClubCommand = editClubCommand;
            _deleteClubCommand = deleteClubCommand;
            _getCitiesCommand = getCitiesCommand;
            _getLeaguesCommand = getLeaguesCommand;
        }



        // GET: Club
        public ActionResult Index([FromQuery] ClubSearch clubSearch)
        {
            try
            {
                var clubs = _getClubsCommand.Execute(clubSearch);
                return View(clubs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: Club/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var club = _getClubCommand.Execute(id);
                return View(club);
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

        // GET: Club/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Cities = _getCitiesCommand.Execute(new CitySearch());
                ViewBag.Leagues = _getLeaguesCommand.Execute(new LeagueSearch());
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Club/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClubDto club)
        {
            try
            {
                var clubInsert = new ClubDto
                {
                    Name = club.Name,
                    CityId = club.CityId,
                    LeagueId = club.LeagueId
                };
                _addClubCommand.Execute(clubInsert);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Club/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var Club = _getClubCommand.Execute(id);
                ViewBag.Cities = _getCitiesCommand.Execute(new CitySearch());
                ViewBag.Leagues = _getLeaguesCommand.Execute(new LeagueSearch());
                return View(new ClubDto
                {
                    LeagueId = Club.LeagueId,
                    CityId = Club.CityId,
                    Id = Club.Id,
                    Name = Club.Name
                });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Club/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClubDto clubDto)
        {
            try
            {
                // TODO: Add update logic here
                var clubEdit = new ClubDto
                {
                    Id = id,
                    LeagueId = clubDto.LeagueId,
                    Name = clubDto.Name,
                    CityId = clubDto.CityId
                };
                _editClubCommand.Execute(clubEdit);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Club/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteClubCommand.Execute(id);
                TempData["success"] = "Movie successfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}