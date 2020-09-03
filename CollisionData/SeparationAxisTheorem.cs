using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.CollisionData
{
    public struct SeparationAxisTheorem
    {
        float min;
        float max;

        private static SeparationAxisTheorem GetInterval(Rectangle rectangle, Vector2 axis)
        {
            SeparationAxisTheorem result;
            //The Maximum and Minimum parts of the rectangle
            Vector2 min, max;
            min.X = rectangle.X;
            min.Y = rectangle.Y;
            max.X = rectangle.X + rectangle.Width;
            max.Y = rectangle.Y + rectangle.Height;
            //Uses the min and max points to build a set of vertices
            Vector2[] vertsOfRect = { new Vector2(min.X, min.Y), new Vector2(min.X, max.Y), new Vector2(max.X, max.Y), new Vector2(max.X, min.Y) };

            //Projects each vertex onto the axis, store the smallest and largest values
            result.min = result.max = Vector2.Dot(axis, vertsOfRect[0]);
            for (int i = 1; i < 4; i++)
            {
                float projection = Vector2.Dot(axis, vertsOfRect[i]);
                if (projection < result.min)
                {
                    result.min = projection;
                }
                if (projection > result.max)
                {
                    result.max = projection;
                }
            }
            return result;
        }
        /// <summary>
        /// Tests if the two intervals overlap
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <param name="axis"></param>
        /// <returns>returns whether or not the two intervals overlap with each other</returns>
        private static bool OverlapOnAxis(Rectangle rect1,Rectangle rect2,Vector2 axis)
        {
            SeparationAxisTheorem a = GetInterval(rect1, axis);
            SeparationAxisTheorem b = GetInterval(rect2, axis);
            return ((b.min <= a.max) && (a.min <= b.max));
        }
        /// <summary>
        /// Tests rectangle collision between two Rectangles using (S.A.T) 
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns>returns whether or not the two rectangles have collided</returns>
        public static bool RectangleRectangleSAT(Rectangle rect1,Rectangle rect2)
        {
            //X-axis & Y-axis(Axes to test)
            Vector2[] axisToTest = { new Vector2(1, 0), new Vector2(0, 1) };
            for (int i = 0; i < 2; i++)
            { 
                //Intervals don't overlap, separating axis found
                if (!OverlapOnAxis(rect1,rect2,axisToTest[i]))
                {
                    //No collision Has taken place
                    return false;
                }
            }
            //All intervals Overlap Separating axis not found
            return true;
        }
    }
}
