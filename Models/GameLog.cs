namespace battleships.Models
{
    public class GameLog
    {
        public PlayerLog p1 { get; set; }
        public PlayerLog p2 { get; set; }
        
        public GameLog(Player x, Player y)
        {
            this.p1 = new PlayerLog(x.map.grid);
            this.p2 = new PlayerLog(y.map.grid);
        }

        public void LogTurn(string p, Turn turn)
        {
            if (p == "p1")
                this.p1.turns.Add(turn);
            else
                this.p2.turns.Add(turn);
        }
    }
}