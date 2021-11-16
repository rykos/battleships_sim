using System;
using System.Collections.Generic;
using System.Linq;
using battleships.Models;

namespace battleships.helpers
{
    public class GameHelper : IGameHelper
    {
        private Random rnd = new Random();

        public GameLog RunGame()
        {
            Player p1 = new Player();
            Player p2 = new Player();
            GameLog gl = new GameLog(p1, p2);

            while (true)
            {
                gl.LogTurn("p1", p1.Fire(p2));
                if (p2.Lost())
                    break;
                gl.LogTurn("p2", p2.Fire(p1));
                if (p1.Lost())
                    break;
            }
            return gl;
        }
    }

    public interface IGameHelper
    {
        GameLog RunGame();
    }
}