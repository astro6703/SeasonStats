using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeasonStats.Model;

namespace SeasonStats.Controllers
{
    [Route("api/Player")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository repository;

        public PlayerController(IPlayerRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IEnumerable<Player>> GetAll()
        {
            return await repository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var player = new Player(name);

            if ((await repository.GetOneAsync(name)) != null)
                return BadRequest($"Player {name} already exists");

            await repository.SaveOneAsync(player);

            return RedirectToAction("Index", "Home");
        }
        
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("all")]
        public async Task<ActionResult> ViewAll()
        {
            ViewBag.Players = await GetAll();

            return View("AllPlayers");
        }
    }
}