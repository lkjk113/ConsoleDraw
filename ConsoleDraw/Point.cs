using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw
{
    public class Point
    {

        public int X { get; set; }
        public int Y { get; set; }
        public char Value { get; set; } = ' ';
        public string GroupNo { get; set; }

        public Point(int x, int y, char value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public Point(int x, int y, char value, string groupNo)
        {
            X = x;
            Y = y;
            Value = value;
            GroupNo = groupNo;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
