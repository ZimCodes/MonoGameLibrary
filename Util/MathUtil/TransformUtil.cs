using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util.MathUtil
{
    public class TransformUtil
    {
        public TransformUtil(){}
        //* WARNING! Possibility of obtaining a Gimbal Lock->(Euler Angle Transform only)
        #region (World Transform) Euler Angle Transformation
        /// <summary>
        /// Matrix Transformation with Scale, Euler Angle Rotation, & Translation 
        /// </summary>
        /// <param name="scale">scaling</param>
        /// <param name="yawpitchroll">X: yaw; Y: pitch; Z: roll (in degrees)</param>
        /// <param name="translation">position to move</param>
        /// <returns>returns a Matrix Transformation in World Space</returns>
        public static Matrix EulerAngleTransformation(float scale, Vector3 yawpitchroll, Vector3 translation)
        {
            return Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(yawpitchroll.X), MathHelper.ToRadians(yawpitchroll.Y), MathHelper.ToRadians(yawpitchroll.Z)) * Matrix.CreateTranslation(translation);
        }
        /// <summary>
        /// Matrix Transformation with Scale, Euler Angle Rotation, & Translation 
        /// </summary>
        /// <param name="scale">scaling</param>
        /// <param name="yaw">degrees to rotate in X-axis</param>
        /// <param name="pitch">degrees to rotate in Y-axis</param>
        /// <param name="roll">degrees to rotate in Z-axis</param>
        /// <param name="Translation">position to move</param>
        /// <returns>returns a Matrix Transformation in World Space</returns>
        public static Matrix EulerAngleTransformation(float scale, float yaw, float pitch, float roll, Vector3 Translation)
        {
            return Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(yaw), MathHelper.ToRadians(pitch), MathHelper.ToRadians(roll)) * Matrix.CreateTranslation(Translation);
        }
        public static Matrix EulerAngleTransformation(float scale, float yaw, float pitch, float roll, float x, float y, float z)
        {
            return Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(yaw), MathHelper.ToRadians(pitch), MathHelper.ToRadians(roll)) * Matrix.CreateTranslation(x, y, z);
        }
        #endregion
        #region (World Transform) Axis Angle Transformation
        /// <summary>
        /// Matrix Transformation with Scale, Axis Angle Rotation, & Translation 
        /// </summary>
        /// <param name="scale">scaling</param>
        /// <param name="axis">Arbitrary Axis to rotate around</param>
        /// <returns>returns Matrix Transformation in World Space</returns>
        public static Matrix AxisAngleTransformation(float scale, Vector3 axis, float degrees, Vector3 translation)
        {
            return Matrix.CreateScale(scale) * AxisAngle(axis, degrees) * Matrix.CreateTranslation(translation);
        }
        public static Matrix AxisAngleTransformation(float scale, Vector3 axis, float degrees, float translationX, float translationY, float translationZ)
        {
            return Matrix.CreateScale(scale) * AxisAngle(axis, degrees) * Matrix.CreateTranslation(translationX, translationY, translationZ);
        }
        #endregion
        #region (World Transform) Regular Matrix Transformation
        /// <summary>
        /// Matrix Transformation with Scaling, Rotation on X,Y,Z axis, Translation
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="degreesXaxis"></param>
        /// <param name="degreesYaxis"></param>
        /// <param name="degreesZaxis"></param>
        /// <param name="translation"></param>
        /// <returns>Matrix Transformation in World Space</returns>
        public static Matrix MatrixTransformation(float scale, float degreesXaxis, float degreesYaxis, float degreesZaxis, Vector3 translation)
        {
            return Matrix.CreateScale(scale) * Matrix.CreateRotationX(MathHelper.ToDegrees(degreesXaxis)) * Matrix.CreateRotationY(MathHelper.ToDegrees(degreesYaxis)) * Matrix.CreateRotationZ(MathHelper.ToDegrees(degreesZaxis)) * Matrix.CreateTranslation(translation);
        }
        public static Matrix MatrixTransformation(float scale, float degreesXaxis, float degreesYaxis, float degreesZaxis, float translationX, float translationY, float translationZ)
        {
            return Matrix.CreateScale(scale) * Matrix.CreateRotationX(MathHelper.ToDegrees(degreesXaxis)) * Matrix.CreateRotationY(MathHelper.ToDegrees(degreesYaxis)) * Matrix.CreateRotationZ(MathHelper.ToDegrees(degreesZaxis)) * Matrix.CreateTranslation(translationX, translationY, translationZ);
        }
        #endregion

        /// <summary>
        /// Provides new Vector2 coordinates for a 2D rotation (CounterClockwise)
        /// </summary>
        /// <param name="vector">Vector2</param>
        /// <param name="degrees">Number of degrees you want to rotate</param>
        /// <returns>returns a new Vector2 with a 2D rotation</returns>
        public static Vector2 TwoDRotation(Vector2 vector, float degrees)
        {
            Matrix tempmatrix = new Matrix(
                new Vector4((float)Math.Cos(MathHelper.ToRadians(degrees)), -(float)Math.Sin(MathHelper.ToRadians(degrees)), 0, 0),
                new Vector4((float)Math.Sin(MathHelper.ToRadians(degrees)), (float)Math.Cos(MathHelper.ToRadians(degrees)), 0, 0),
                Vector4.Zero,
                Vector4.Zero
                );

            return MultiplyVectAndMatrix(vector, tempmatrix);
        }
        /// <summary>
        /// Multiply a Vector with a Matrix
        /// </summary>
        /// <param name="vector">Vector to multiply</param>
        /// <param name="matrix">Matrix to multiply</param>
        /// <returns>returns a Vector with the product of a Vector and Matrix</returns>
        #region Multiply Vector and Matrix (Vector)
        public static Vector2 MultiplyVectAndMatrix(Vector2 vector, Matrix matrix)
        {
            Vector2 Result;
            Result.X = vector.X * matrix.M11 + vector.Y * matrix.M12;
            Result.Y = vector.X * matrix.M21 + vector.Y * matrix.M22;
            return Result;
            
        }
        public static Vector3 MultiplyVectAndMatrix(Vector3 vector, Matrix matrix)
        {
            Vector3 Result;
            Result.X = vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13;
            Result.Y = vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23;
            Result.Z = vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33;
            return Result;

        }
        public static Vector4 MultiplyVectAndMatrix(Vector4 vector, Matrix matrix)
        {
            Vector4 Result;
            Result.X = vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13 + vector.W * matrix.M14;
            Result.Y = vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23 + vector.W * matrix.M24;
            Result.Z = vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33 + vector.W * matrix.M34;
            Result.W = vector.X * matrix.M41 + vector.Y * matrix.M42 + vector.Z * matrix.M43 + vector.W * matrix.M44;
            return Result;

        }
#endregion
        /// <summary>
        /// Multiply a Matrix with a Vector and places result in Translation Vector(row Major)
        /// </summary>
        /// <param name="vector">Vector</param>
        /// <param name="matrix">Matrix</param>
        /// <returns>returns a Matrix with the product of a Matrix and Vector (row Major)</returns>
        #region Multiply Vector and Matrix (Matrix)
        public static Matrix MultiplyMatrixAndVect(Matrix matrix,Vector2 vector)
        {
            Matrix Result= new Matrix();
            Result.M41 = vector.X * matrix.M11 + vector.Y * matrix.M12;
            Result.M42 = vector.X * matrix.M21 + vector.Y * matrix.M22;
            return Result;
        }
        public static Matrix MultiplyMatrixAndVect(Matrix matrix, Vector3 vector)
        {
            Matrix Result = new Matrix();
            Result.M41 = vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13;
            Result.M42 = vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23;
            Result.M43 = vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33;
            return Result;
        }
        public static Matrix MultiplyMatrixAndVect(Matrix matrix, Vector4 vector)
        {
            Matrix Result = new Matrix();
            Result.M41 = vector.X * matrix.M11 + vector.Y * matrix.M12 + vector.Z * matrix.M13 + vector.W * matrix.M14;
            Result.M42 = vector.X * matrix.M21 + vector.Y * matrix.M22 + vector.Z * matrix.M23 + vector.W * matrix.M24;
            Result.M43 = vector.X * matrix.M31 + vector.Y * matrix.M32 + vector.Z * matrix.M33 + vector.W * matrix.M34;
            Result.M44 = vector.X * matrix.M41 + vector.Y * matrix.M42 + vector.Z * matrix.M43 + vector.W * matrix.M44;
            return Result;
        }
        #endregion
        /// <summary>
        /// Instead of rotating one axis at a time, then combining the rotation, axis angle rotation rotates by some angle around an arbitrary axis.
        /// </summary>
        /// <param name="axis">Arbitrary Axis to Rotate around</param>
        /// <param name="degrees">rotation angle in degrees</param>
        /// <returns>returns a matrix that rotates by some angle around an arbitrary axis</returns>
        public static Matrix AxisAngle(Vector3 axis, float degrees)
        {
            degrees = MathHelper.ToRadians(degrees);
            float c = (float)Math.Cos(degrees);
            float s = (float)Math.Sin(degrees);
            float t = 1.0f - c;

            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            if (axis.LengthSquared() != 1)
            {
                float normalize = 1 / axis.Length();
                x *= normalize;
                y *= normalize;
                z *= normalize;
            }
            return new Matrix(
                new Vector4(t*(x*x)+c,t*x*y + s*z, t*x*z - s*y, 0),
                new Vector4(t*x*y - s*z, t*(y*y)+c,t*y*z +s*x,0),
                new Vector4(t*x*z +s*y, t*y*z - s*x, t*(z*z) + c,0),
                Vector4.UnitW
                );
        }
        
    }
}
