using System.Collections.Generic;
using System.Linq;

namespace battleships.Models
{
    public class Map
    {
        public CellStatus[][] grid;
        public CellStatus[][] shootingGrid;

        public Map()
        {
            this.grid = this.CreateEmptyGrid();
            this.shootingGrid = this.CreateEmptyGrid();
        }

        public int GetSize() => this.grid?.GetLength(0) ?? -1;

        private CellStatus[][] CreateEmptyGrid(int size = 10)
        {
            CellStatus[][] res = new CellStatus[size][];
            for (int i = 0; i < size; i++)
            {
                res[i] = new CellStatus[size];
                for (int j = 0; j < size; j++)
                {
                    res[i][j] = CellStatus.water;
                }
            }
            return res;
        }

        public bool PlaceShip(Vector2 pos, int size, Vector2 rotation)
        {
            if (CanPlaceShip(pos, size, rotation))
            {
                for (int i = 0; i < size; i++)
                    grid[pos.X + (rotation.X * i)][pos.Y + (rotation.Y * i)] = CellStatus.ship;
                return true;
            }
            return false;
        }

        private bool CanPlaceShip(Vector2 pos, int size, Vector2 rotation)
        {
            for (int i = 0; i < size; i++)
            {
                Vector2 newPosition = new Vector2(pos.X + (rotation.X * i), pos.Y + (rotation.Y * i));
                // Cell is not water
                if (!(WithinBoundry(newPosition) && grid[newPosition.X][newPosition.Y] == CellStatus.water))
                    return false;
                // Sibling cells are also water
                for (int x = -1; x < 2; x++)
                    for (int y = -1; y < 2; y++)
                    {
                        Vector2 sp = new Vector2(newPosition.X + x, newPosition.Y + y);
                        //If they exist, they must be water
                        if (WithinBoundry(sp) && grid[sp.X][sp.Y] != CellStatus.water)
                            return false;
                    }
            }
            return true;
        }

        public bool WithinBoundry(Vector2 pos, int size = 10)
        {
            if (pos.X >= 0 && pos.X < size && pos.Y >= 0 && pos.Y < size)
                return true;
            else
                return false;
        }

        public List<int> GetValidShotsInCol(CellStatus[] col)
        {
            List<int> pos = new List<int>();
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i] == CellStatus.water)
                    pos.Add(i);
            }
            return pos;
        }

        public bool ColIsValidTarget(CellStatus[] col)
        {
            if (col.FirstOrDefault(x => x == CellStatus.water) != default)
                return true;
            else
                return false;
        }

        public void UpdateShootingGrid(CellStatus cs, Vector2 pos)
        {
            if (cs == CellStatus.ship || cs == CellStatus.destroyed)
                shootingGrid[pos.X][pos.Y] = CellStatus.hit;
            else if (cs == CellStatus.water)
                shootingGrid[pos.X][pos.Y] = CellStatus.miss;
        }
    }
}