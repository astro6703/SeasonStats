using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeasonStats.Model;

namespace SeasonStats.Controllers
{
    [Route("api/Match")]
    public class MatchController : Controller
    {
        private readonly IMatchRepository matchRepository;
        private readonly IPlayerRepository playerRepository;

        public MatchController(IMatchRepository matchRepository, IPlayerRepository playerRepository)
        {
            this.matchRepository = matchRepository ?? throw new ArgumentNullException(nameof(matchRepository));
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<Match>> GetAll()
        {
            return await matchRepository.GetAllAsync();
        }

        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create/bestofn/")]
        public IActionResult BestOfN(int bestOf)
        {
            ViewBag.BestOf = bestOf;

            return View("BestOfNView");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name1, string name2, int[] scores)
        {
            if (name1 == null) throw new ArgumentNullException(nameof(name1));
            if (name2 == null) throw new ArgumentNullException(nameof(name2));

            var player1 = await playerRepository.GetOneAsync(name1);
            var player2 = await playerRepository.GetOneAsync(name2);

            if (player1 == null) return NotFound($"Player {name1} was not found");
            if (player2 == null) return NotFound($"Player {name2} was not found");

            var match = new Match(scores.Length / 2);

            for (int i = 0; i < scores.Length; i += 2)
            {
                var set = new Set(player1, player2, scores[i], scores[i + 1]);
                if (set.IsValid() && !match.IsFinished())
                    match.AddSet(set);
            }

            if (match.IsFinished())
                await matchRepository.SaveOneAsync(match);

            return RedirectToAction("Index", "Home");
        }
        
        [Route("all")]
        public async Task<ActionResult> ViewAll()
        {
            ViewBag.Matches = await GetAll();

            return View("AllMatches");
        }
    }
}