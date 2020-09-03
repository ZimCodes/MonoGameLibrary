using Microsoft.Xna.Framework;
using MonoGameLibrary.Custom_Classes.Shapes2D;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.CollisionData
{
    public class Collisions
    {
        public Collisions()
        {

        }
        /// <summary>
        /// Circle to Circle Collision
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>returns whether or not a Circle has collided with another Circle</returns>
        public static bool CircleCircle(Circle c1,Circle c2)
        {
            //Create a line between origins of circles
            Line line = new Line(c1.Position, c2.Position);
            float radiisum = c1.Radius + c2.Radius;
            return Line.LengthSquared(line) <= radiisum * radiisum;
        }
        /// <summary>
        /// Rectangle to Circle Collision
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="rectangle"></param>
        /// <returns>returns whether or not a Rectangle has collided with Circle</returns>
        public static bool CircleRectangle(Circle circle, Rectangle rectangle)
        {
            //The Maximum and Minimum parts of the rectangle
            Vector2 min,max;
            min.X = rectangle.X;
            min.Y = rectangle.Y;
            max.X = rectangle.X + rectangle.Width;
            max.Y = rectangle.Y + rectangle.Height;

            //finds the closest point on rectangle to circle  
            Point closestPoint = circle.Position;

            closestPoint.X = (closestPoint.X < min.X) ? (int)min.X : closestPoint.X;
            closestPoint.X = (closestPoint.X > max.X) ? (int)max.X:  closestPoint.X;
            closestPoint.Y = (closestPoint.Y < min.Y) ? (int)min.Y : closestPoint.Y;
            closestPoint.Y = (closestPoint.Y > max.Y) ? (int)max.Y : closestPoint.Y;
            
            
            Line line = new Line(circle.Position,closestPoint);
            
            return Line.LengthSquared(line) <= circle.Radius * circle.Radius;
        }
        /// <summary>
        /// Rectangle to Rectangle Collision
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns>returns whether or not Two Rectangles has collided</returns>
        public static bool RectangleRectangle(Rectangle rect1,Rectangle rect2)
        {

            Vector2 aMin, aMax,bMin,bMax;
            aMin.X = rect1.Location.X;
            aMin.Y = rect1.Location.Y;
            aMax.X = rect1.Location.X + rect1.Width;
            aMax.Y = rect1.Location.Y + rect1.Height;

            bMin.X = rect2.Location.X;
            bMin.Y = rect2.Location.Y;
            bMax.X = rect2.Location.X + rect2.Width;
            bMax.Y = rect2.Location.Y + rect2.Height;

            bool overX = ((bMin.X <= aMax.X) && (aMin.X <= bMax.X));
            bool overY = ((bMin.Y <= aMax.Y) && (aMin.Y <= bMax.Y));
            return overX && overY;
        }

        #region Point Containment Test
        /// <summary>
        /// Tells whether or not a point intersects a line 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="line"></param>
        /// <returns>returns whether or not a point collided with a line</returns>
        public static bool PointOnLine(Point point, Line line)
        {
            if (point.X > line.End.X && point.Y > line.End.Y || point.X < line.Start.X && point.Y < line.Start.Y)
            {
                return false;
            }
            //Slope intercept formula y=mx+b
            //Finds the Slope 
            float dy = line.End.Y - line.Start.Y;
            float dx = line.End.X - line.Start.X;
            float m = dy / dx;

            //Y-Intercept
            float b = line.Start.Y - m * line.Start.X;

            //Check line equation

            return CMP.IsEq(point.Y, m * point.X + b);
        }
        /// <summary>
        /// Tells whether or not a point has collided with a circle
        /// </summary>
        /// <param name="point"></param>
        /// <param name="circle"></param>
        /// <returns>returns whether or not a point has collided with a circle</returns>
        public static bool PointInCircle(Point point, Circle circle)
        {
            Line line = new Line(point, circle.Position);
            if (Line.LengthSquared(line) <= circle.Radius * circle.Radius)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Tells whether or not a point has collided with a Rectangle
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rectangle"></param>
        /// <returns>returns whether or not a point has collided with a Rectangle</returns>
        public static bool PointInRectangle(Point point, Rectangle rectangle)
        {
            Vector2 min = new Vector2(rectangle.X, rectangle.Y);
            Vector2 max = new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            if (min.X <= point.X && min.Y <= point.Y && point.X <= max.X && point.Y <= max.Y)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Line Intersection
        /// <summary>
        /// Tells whether or not a line has collided with a circle
        /// </summary>
        /// <param name="line"></param>
        /// <param name="circle"></param>
        /// <returns>Tells whether or not a line has collided with a circle</returns>
        public static bool LineCircle(Line line, Circle circle)
        {
            Vector2 lineStart, lineEnd, circlevector;
            //Point to Vector
            lineStart.X = line.Start.X;
            lineStart.Y = line.Start.Y;
            lineEnd.X = line.End.X;
            lineEnd.Y = line.End.Y;
            circlevector.X = circle.Position.X;
            circlevector.Y = circle.Position.Y;
            Vector2 ab = lineEnd - lineStart;


            float t = Vector2.Dot(circlevector - lineStart, ab) / Vector2.Dot(ab, ab);
            if (t < 0 || t > 1)
            {
                return false;
            }
            Vector2 result = lineStart + ab * t;
            Point closestPoint;
            closestPoint.X = (int)result.X;
            closestPoint.Y = (int)result.Y;

            Line circleToClosest = new Line(circle.Position, closestPoint);
            return Line.LengthSquared(circleToClosest) <= circle.Radius * circle.Radius;
        }


        #endregion
    }
}
