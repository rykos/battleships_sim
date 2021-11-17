using System;
using System.Collections.Generic;

namespace battleships.Models
{
    public class Player
    {
        Random rnd = new Random();
        public Map map = new Map();
        public int shipsLeft = 0;
        Vector2 shipPos = null;
        Vector2 lastHit = null;
        Vector2 shotDir = null;
        Vector2 origShotDir = null;

        public Player()
        {
            SetShips();
        }

        public Turn Fire(Player target)
        {
            Vector2 shotPosition;
            CellStatus cs;

            if (shipPos != null)
                shotPosition = FindNextShipCell();
            else
                shotPosition = SelectRandomValidCell();

            cs = target.TakeHit(shotPosition);
            // Set own shooting grid
            map.UpdateShootingGrid(cs, shotPosition);

            RegisterShot(shotPosition, cs);
            return new Turn(shotPosition, cs);
        }

        private Vector2 SelectRandomValidCell()
        {
            List<int> possibleColumns = new List<int>();
            for (int i = 0; i < map.shootingGrid.GetLength(0); i++)
            {
                if (map.ColIsValidTarget(map.shootingGrid[i]))
                    possibleColumns.Add(i);
            }
            int selectedCol = possibleColumns[rnd.Next(0, possibleColumns.Count)];
            List<int> possibleShots = map.GetValidShotsInCol(map.shootingGrid[selectedCol]);

            return new Vector2(selectedCol, possibleShots[rnd.Next(0, possibleShots.Count)]);
        }

        private Vector2 FindNextShipCell()
        {
            Vector2 shotPos = this.lastHit + this.shotDir;
            while (!map.WithinBoundry(shotPos) || map.shootingGrid[shotPos.X][shotPos.Y] != CellStatus.water)
            {
                this.shotDir = Vector2.NextDirection(this.shotDir);
                if (this.shotDir == null)
                {
                    this.lastHit = this.shipPos;
                    this.origShotDir = Vector2.NextDirection(origShotDir);
                    this.shotDir = this.origShotDir;
                    // return this.SelectRandomValidCell();
                }
                shotPos = this.lastHit + this.shotDir;
            }
            return shotPos;
        }

        private void RegisterShot(Vector2 shotPosition, CellStatus cs)
        {
            this.lastHit = shotPosition;
            if (shipPos == null && cs == CellStatus.ship)
            {
                ///Ship detected
                this.shipPos = shotPosition;
                this.shotDir = Vector2.Directions[0];
                this.origShotDir = this.shotDir;
            }
            else if (shipPos != null && cs == CellStatus.water)
            {
                //Missed next part of ship
                this.lastHit = this.shipPos;
                this.shotDir = Vector2.Directions[0];
            }
            else if (shipPos != null && cs == CellStatus.destroyed)
            {
                ///Ship was sunk
                shipPos = null;
            }
        }

        /// <returns>Ship when hit, Destroyed when whole ship sunk, Water when missed</returns>
        public CellStatus TakeHit(Vector2 pos)
        {
            CellStatus cs = this.map.grid[pos.X][pos.Y];
            if (cs == CellStatus.ship)
            {
                this.map.grid[pos.X][pos.Y] = CellStatus.destroyed;
                this.shipsLeft--;
                if (ShipSunk(pos))
                    return CellStatus.destroyed;
                else
                    return CellStatus.ship;
            }
            return CellStatus.water;
        }

        private bool ShipSunk(Vector2 shipPos)
        {
            Vector2 curPos = shipPos;
            Vector2 dir = Vector2.Directions[0];
            int i = 0;
            while (true)
            {
                curPos = curPos + dir;
                if (!this.map.WithinBoundry(curPos) || this.map.grid[curPos.X][curPos.Y] == CellStatus.water)
                {
                    //Break if checked all directions
                    if (i == 3)
                        break;
                    curPos = shipPos;
                    //change seeking direction
                    i++;
                    dir = Vector2.Directions[i];
                }
                else if (this.map.grid[curPos.X][curPos.Y] == CellStatus.ship)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Lost() => this.shipsLeft <= 0;

        private void SetShips()
        {
            int[] ships = new int[] { 5, 4, 3, 3, 2 };
            int i = ships.Length - 1;
            while (i >= 0)
            {
                if (map.PlaceShip(new Vector2(rnd.Next(0, 10), rnd.Next(0, 10)), ships[i], Vector2.RandomDirection()))
                {
                    this.shipsLeft += ships[i];
                    i--;
                }
            }
        }
    }
}