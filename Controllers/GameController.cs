using battleships.helpers;
using battleships.Models;
using Microsoft.AspNetCore.Mvc;

namespace battleships_sim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameHelper gameHelper;
        public GameController(IGameHelper gameHelper)
        {
            this.gameHelper = gameHelper;
        }

        [HttpGet]
        public GameLog GetGame()
        {
            return gameHelper.RunGame();
        }
    }
}