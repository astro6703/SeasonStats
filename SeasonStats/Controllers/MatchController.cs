using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeasonStats.Model;

namespace SeasonStats.Controllers
{
    [Produces("application/json")]
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

        [HttpPost()]
        public async Task<IActionResult> CreateMatch(string name1, string name2, int[] scores)
        {
            var player1 = await playerRepository.GetOneAsync(name1);
            var player2 = await playerRepository.GetOneAsync(name2);

            if (player1 == null || player2 == null)
                throw new KeyNotFoundException("One or two players not found");

            var match = new Match(scores.Length / 2);

            for (int i = 0; i < scores.Length; i += 2)
            {
                var set = new Set(player1, player2, scores[i], scores[i + 1]);

                match.AddSet(set);
            }

            await matchRepository.SaveOneAsync(match);

            return RedirectToAction("Index", "Home");
        }
    }
}