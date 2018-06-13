using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw
{
    public class Canvas
    {
        public List<Point> Points { get; set; }
        public string DefaultGroupNo = Guid.NewGuid().ToString();

        public Point this[int x, int y]
        {
            get
            {
                return Points.FirstOrDefault(r => r.X == x && r.Y == y);
            }
        }

        public Canvas()
        {
            Points = new List<Point>();
        }
    }
}
