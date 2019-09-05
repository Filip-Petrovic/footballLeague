using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
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

        public ClubController(IGetClubCommand getClubCommand, IGetClubsCommand getClubsCommand, IAddClubCommand addClubCommand, IEditClubCommand editClubCommand, IDeleteClubCommand deleteClubCommand)
        {
            _getClubCommand = getClubCommand;
            _getClubsCommand = getClubsCommand;
            _addClubCommand = addClubCommand;
            _editClubCommand = editClubCommand;
            _deleteClubCommand = deleteClubCommand;
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
            return View();
        }

        // POST: Club/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Club/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Club/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Club/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}