using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Custom_Classes.Shapes2D
{
    public struct Line
    {
        public Point Start;
        public Point End;

        public Line(Point start,Point end)
        {
            this.Start = start;
            this.End = end;
            
        }
        /// <summary>
        /// Finds the Length of a line segment
        /// </summary>
        /// <param name="line">line to find the length of</param>
        /// <returns>returns the length of the line</returns>
        public static float Length(Line line)
        {
            Point point = line.End - line.Start;
            return (float)Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
        }
        /// <summary>
        /// Finds the Length Squared of a line segment
        /// </summary>
        /// <param name="line">line to find the length of</param>
        /// <returns>returns the length of the line squared</returns>
        public static float LengthSquared(Line line)
        {
            Point point = line.End - line.Start;
            return (float)Math.Pow(point.X, 2) + (float)Math.Pow(point.Y, 2);
        }
        public override string ToString()
        {
            return String.Format("Start:{0}, End: {1}",this.Start,this.End);
        }
    }
}
