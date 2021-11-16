namespace battleships.Models
{
    public struct Turn
    {
        public Turn(Vector2 position, CellStatus cellStatus)
        {
            Position = position;
            CellStatus = cellStatus;
        }

        public Vector2 Position { get; set; }
        public CellStatus CellStatus { get; set; }
    }
}