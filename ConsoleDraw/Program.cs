using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDraw
{
    class Program
    {
        static Canvas Canvas = new Canvas();

        static List<Line> Lines = new List<Line>();

        static void Main(string[] args)
        {
            string cmd = "";

            List<char[]> canvas = new List<char[]>();

            for (; ; )
            {
                try
                {


                    cmd = Console.ReadLine().Trim();
                    if (cmd.Length > 0)
                    {
                        var keys = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        switch (keys[0].ToUpper())
                        {
                            case "C":
                                DrawCanvas(keys);
                                break;
                            case "L":
                                DrawLine(keys);
                                break;
                            case "R":
                                DrawRentangle(keys);
                                break;
                            case "B":
                                BucketFill(keys);
                                break;
                            case "Q":
                                Environment.Exit(0);
                                break;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Render()
        {
            var points = Canvas.Points.OrderBy(r => r.Y);
            for (int y = points.Min(o => o.Y); y <= points.Max(o => o.Y); y++)
            {
                var row = points.Where(r => r.Y == y);
                var cells = row.OrderBy(r => r.X);
                foreach (var cell in cells)
                {
                    Console.Write(cell.Value);
                }
                Console.Write(Environment.NewLine);
            }
        }

        private static void DrawCanvas(string[] keys)
        {
            int w = int.Parse(keys[1]) + 2;
            int h = int.Parse(keys[2]) + 2;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (y == 0 || y == h - 1)
                    {
                        Canvas.Points.Add(new Point(x, y, '-', Canvas.DefaultGroupNo));
                    }
                    else
                    {
                        if (x == 0 || x == w - 1)
                        {
                            Canvas.Points.Add(new Point(x, y, '|', Canvas.DefaultGroupNo));
                        }
                        else
                        {
                            Canvas.Points.Add(new Point(x, y, ' ', Canvas.DefaultGroupNo));
                        }
                    }
                }
            }
            Render();
        }



        private static void DrawLine(string[] keys)
        {
            int x1 = int.Parse(keys[1]);
            int y1 = int.Parse(keys[2]);
            int x2 = int.Parse(keys[3]);
            int y2 = int.Parse(keys[4]);

            Line line = new Line();

            string newNo = Guid.NewGuid().ToString();

            if (x1 == x2 && y1 != y2) //Vertical
            {
                for (int y = y1; y <= y2; y++)
                {
                    Canvas[x1, y].Value = 'x';
                    line.Points.Add(Canvas[x1, y]);
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //TODO: This function here has two many situations need to consider, such as line cross line ,line cross rectangle etc
                //So just hard code this time
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (Canvas[x1, y1 - 1].Value == 'x' && Canvas[x1, y2 + 1].Value == '-')
                {
                    var conjointLine = Lines.FirstOrDefault(r => r.Points.Contains(Canvas[x1, y1 - 1]));
                    Point rectB = new Point(1, y1);
                    Point rectE = new Point(x1 - 1, y2);

                    var pts = Canvas.Points.Where(r => r.X >= rectB.X && r.X <= rectE.X && r.Y >= rectB.Y && r.Y <= rectE.Y);
                    foreach (var pt in pts)
                    {
                        pt.GroupNo = newNo;
                    }
                }
            }
            else if (y1 == y2 && x1 != x2) //Horizontal
            {
                for (int x = x1; x <= x2; x++)
                {
                    Canvas[x, y1].Value = 'x';
                    line.Points.Add(Canvas[x, y1]);
                }
            }
            else
            {
                throw new Exception("Unsupported command!");
            }


            Render();
        }


        private static void DrawRentangle(string[] keys)
        {
            int x1 = int.Parse(keys[1]);
            int y1 = int.Parse(keys[2]);
            int x2 = int.Parse(keys[3]);
            int y2 = int.Parse(keys[4]);

            string newNo = Guid.NewGuid().ToString();

            for (int y = y1; y <= y2; y++)
            {

                for (int x = x1; x <= x2; x++)
                {
                    if (y == y1 || y == y2)
                    {
                        Canvas[x, y].Value = 'x';
                    }
                    else
                    {
                        if (x == x1 || x == x2)
                        {
                            Canvas[x, y].Value = 'x';
                        }
                        else
                        {
                            Canvas[x, y].Value = ' ';
                            Canvas[x, y].GroupNo = newNo;
                        }
                    }
                }
            }

            Render();
        }


        private static void BucketFill(string[] keys)
        {
            int x = int.Parse(keys[1]);
            int y = int.Parse(keys[2]);
            char brush = char.Parse(keys[3]);

            var checkPoint = Canvas[x, y];
            var friends = Canvas.Points.FindAll(r => r.GroupNo == checkPoint.GroupNo && r.Value == checkPoint.Value);
            friends.ForEach(point => { point.Value = brush; });

            Render();
        }

    }
}
