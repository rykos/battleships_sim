using System.Collections.Generic;
using System.Linq;

namespace battleships.Models
{
    public class PlayerLog
    {
        public List<Turn> turns { get; set; } = new List<Turn>();
        public CellStatus[][] map { get; set; }

        public PlayerLog(CellStatus[][] cellStatuses)
        {
            this.map = cellStatuses.Select(a => a.ToArray()).ToArray();
        }
    }
}