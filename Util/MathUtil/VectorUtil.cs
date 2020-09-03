using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util.MathUtil
{
    public class VectorUtil
    {
        public VectorUtil(){}
        #region Vector2 Shortcuts
        private static Vector2 left;
        /// <summary>
        /// Returns a Vector2 with components -1, 0.
        /// </summary>
        public static Vector2 Left
        {
            get {
                if (left == Vector2.Zero)
                {
                    left = new Vector2(-1, 0);
                }
                return left;
            
            }
        }
        private static Vector2 right;
        /// <summary>
        /// Returns a Vector2 with components 1, 0.
        /// </summary>
        public static Vector2 Right
        {
            get
            {
                if (right == Vector2.Zero)
                {
                    right = new Vector2(1,0);
                }
                return right;
            }
        }
        private static Vector2 up;
        /// <summary>
        /// Returns a Vector2 with components 0, -1.
        /// </summary>
        public static Vector2 Up
        {
            get
            {
                if (up == Vector2.Zero)
                {
                    up = new Vector2(0,-1);
                }
                return up;
            }
        }
        private static Vector2 down;
        /// <summary>
        /// Returns a Vector2 with components 0, 1. 
        /// </summary>
        public static Vector2 Down
        {
            get
            {
                if (down == Vector2.Zero)
                {
                    down = new Vector2(0,1);
                }
                return down;
            }
            
        }
        #endregion
        /// <summary>
        /// Finds the Angle between two Vectors
        /// </summary>
        /// <param name="vectorU">First Vector</param>
        /// <param name="vectorV">Second Vector</param>
        /// <param name="decimalplace">Decimal place to round</param>
        /// <returns>returns the number of degrees between two Vectors</returns>
        /// <warning>3 Expensive Trig Costs</warning>
        #region Vector Angles

        public static float VectorAngle(Vector2 vectorU,Vector2 vectorV,int decimalplace)
        {
            float vectors = Vector2.Dot(vectorU,vectorV);
            //multiply values BEFORE square rooting (optimal)
            //This way the square root operation is performed only once instead of twice
            float magnitudeSqr = vectorU.LengthSquared() * vectorV.LengthSquared();
            float magnitude = (float)Math.Sqrt(magnitudeSqr);
            float normal = vectors/magnitude;
            float angleInRadian = (float)Math.Acos(normal);
            float convertToDegrees = MathHelper.ToDegrees(angleInRadian);

            return (float)Math.Round(convertToDegrees,decimalplace);
        }
        public static float VectorAngle(Vector3 vectorU, Vector3 vectorV,int decimalplace)
        {
            float vectors = Vector3.Dot(vectorU, vectorV);
            //multiply values BEFORE square rooting (optimal)
            //This way the square root operation is performed only once instead of twice
            float magnitudeSqr = vectorU.LengthSquared() * vectorV.LengthSquared();
            float magnitude = (float)Math.Sqrt(magnitudeSqr);
            float normal = vectors / magnitude;
            float angleInRadian = (float)Math.Acos(normal);
            float convertToDegrees = MathHelper.ToDegrees(angleInRadian);

            return (float)Math.Round(convertToDegrees, decimalplace);
        }
        public static float VectorAngle(Vector4 vectorU, Vector4 vectorV, int decimalplace)
        {
            float vectors = Vector4.Dot(vectorU, vectorV);
            //multiply values BEFORE square rooting (optimal)
            //This way the square root operation is performed only once instead of twice
            float magnitudeSqr = vectorU.LengthSquared() * vectorV.LengthSquared();
            float magnitude = (float)Math.Sqrt(magnitudeSqr);
            float normal = vectors / magnitude;
            float angleInRadian = (float)Math.Acos(normal);
            float convertToDegrees = MathHelper.ToDegrees(angleInRadian);

            return (float)Math.Round(convertToDegrees, decimalplace);
        }
        #endregion


        /// <summary>
        /// Gives us the Projected Vector of Vector A if Vector A was going in the same direction as Vector B
        /// </summary>
        /// <param name="vectorA">The Vector that will be projecting onto Vector B</param>
        /// <param name="vectorB">The Vector being projected upon</param>
        /// <returns>returns the projected Vector</returns>
        #region Projection A onto B
        public static Vector2 Projection(Vector2 vectorA,Vector2 vectorB)
        {
            float scalarC = (Vector2.Dot(vectorA, vectorB)) / (Vector2.Dot(vectorB, vectorB));
            Vector2 projVector = scalarC * vectorB;
            return projVector;
        }
        public static Vector3 Projection(Vector3 vectorA, Vector3 vectorB)
        {
            float scalarC = (Vector3.Dot(vectorA, vectorB)) / (Vector3.Dot(vectorB, vectorB));
            Vector3 projVector = scalarC * vectorB;
            return projVector;
        }
        public static Vector4 Projection(Vector4 vectorA, Vector4 vectorB)
        {
            float scalarC = (Vector4.Dot(vectorA, vectorB)) / (Vector4.Dot(vectorB, vectorB));
            Vector4 projVector = scalarC * vectorB;
            return projVector;
        }
        #endregion

        /// <summary>
        /// Provides a Vector orthogonal to the Projected Vector
        /// </summary>
        /// <param name="vectorA">The Vector that will be projecting onto Vector B</param>
        /// <param name="vectorB">The Vector being projected upon</param>
        /// <returns>returns a Vector perpendicular to the Projected Vector</returns>
        #region Perpendicular
        public static Vector2 Perpendicular(Vector2 vectorA, Vector2 vectorB)
        {
            return vectorA - Projection(vectorA, vectorB);
        }
        public static Vector3 Perpendicular(Vector3 vectorA, Vector3 vectorB)
        {
            return vectorA - Projection(vectorA, vectorB);
        }
        public static Vector4 Perpendicular(Vector4 vectorA, Vector4 vectorB)
        {
            return vectorA - Projection(vectorA, vectorB);
        }
        #endregion
        /// <summary>
        /// Provides a reflection vector (Vector to Vector version) 
        /// </summary>
        /// <param name="vectorV">A Vector</param>
        /// <param name="vectorN">non-normalized Vector</param>
        /// <returns>returns the reflection Vector</returns>
        #region Reflection Non-normalized Version
        public static Vector2 Reflection(Vector2 vectorV, Vector2 vectorN)
        {
            Vector2 projvector = Projection(vectorV,vectorN);

            return vectorV - (2 *projvector);
        }
        public static Vector3 Reflection(Vector3 vectorV, Vector3 vectorN)
        {
            Vector3 projvector = Projection(vectorV, vectorN);

            return vectorV - (2 * projvector);
        }
        public static Vector4 Reflection(Vector4 vectorV, Vector4 vectorN)
        {
            Vector4 projvector = Projection(vectorV, vectorN);

            return vectorV - (2 * projvector);
        }
        #endregion
        /// <summary>
        /// Provides a reflection vector (Vector to Normalized Vector version) 
        /// </summary>
        /// <param name="vector">Vector</param>
        /// <param name="normal">Normalized Vector</param>
        /// <returns>returns the reflection vector</returns>
        #region Normalize Reflection Version 
        public static Vector2 NormalReflection(Vector2 vector, Vector2 normal)
        {
            Vector2 vectorN = Vector2.Dot(vector,normal) * normal;

            return vector - (2 * vectorN);
        }
        public static Vector3 NormalReflection(Vector3 vector, Vector3 normal)
        {
            Vector3 vectorN = Vector3.Dot(vector, normal) * normal;

            return vector - (2 * vectorN);
        }
        public static Vector4 NormalReflection(Vector4 vector, Vector4 normal)
        {
            Vector4 vectorN = Vector4.Dot(vector, normal) * normal;

            return vector - (2 * vectorN);
        }
#endregion
        /// <summary>
        /// Converts 3D Vectors to 2D
        /// </summary>
        /// <param name="vector">vector to convert</param>
        /// <returns>returns a vector2 from a vector3 </returns>
        public static Vector2 ConvertTo2D(Vector3 vector)
        {
            Vector2 result;
            result.X = vector.X + (2 * vector.Y);
            result.Y = 3 * vector.Z;
            return result;
        }
    }
}
