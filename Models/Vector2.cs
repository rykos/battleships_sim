using System;
using System.Linq;

namespace battleships.Models
{
    public class Vector2
    {
        public static readonly Vector2[] Directions = new Vector2[] {
            new Vector2(1, 0),//R
            new Vector2(-1, 0),//L
            new Vector2(0, 1),//U 
            new Vector2(0, -1)//D
        };
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 RandomDirection()
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 4);
            switch (r)
            {
                case 0:
                    return new Vector2(-1, 0);
                case 1:
                    return new Vector2(1, 0);
                case 2:
                    return new Vector2(0, -1);
                case 3:
                    return new Vector2(0, 1);
                default:
                    return new Vector2(0, 1);
            }
        }

        public static Vector2 NextDirection(Vector2 dir)
        {
            int i = Array.IndexOf(Vector2.Directions, Vector2.Directions.First(x => x.X == dir.X && x.Y == dir.Y));
            i++;
            if (i >= Vector2.Directions.Length)
                return null;
            else
                return Vector2.Directions[i];
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
    }
}