using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util.MathUtil
{
   
    public class MatrixUtil
    {
        
        
        public MatrixUtil(){}
        
        
        /// <summary>
        /// From a 4x4 Matrix, It Deletes a row and column then returns a 3x3 Matrix  
        /// </summary>
        /// <param name="matrix">4x4 Matrix</param>
        /// <param name="row">row to delete</param>
        /// <param name="column">column to delete</param>
        /// <returns>returns a new 3x3 Matrix in a 4x4 Matrix format</returns>
        #region Cut 4x4Matrix to 3x3Matrix
        public static Matrix CutMatrix(Matrix matrix,int row,int column)
        {

            
            //M11
            if (row == 1 && column == 1)
            {
                //Converts to a 3x3 matrix
                matrix = new Matrix(
                new Vector4(matrix.M22,matrix.M23,matrix.M24,0),
                new Vector4(matrix.M32, matrix.M33, matrix.M34,0),
                new Vector4(matrix.M42, matrix.M43, matrix.M44,0),
                Vector4.UnitW
                );
            }
            //M12
            if (row == 1 && column == 2)
            {
                //Converts to a 3x3 matrix
                matrix = new Matrix(
                new Vector4(matrix.M21, matrix.M23, matrix.M24,0),
                new Vector4(matrix.M31, matrix.M33, matrix.M34,0),
                new Vector4(matrix.M41, matrix.M43, matrix.M44,0),
                Vector4.UnitW
                );
            }
            //M13
            if (row == 1 && column == 3)
            {
                //Converts to a 3x3 matrix
                matrix = new Matrix(
                new Vector4(matrix.M21, matrix.M22, matrix.M24,0),
                new Vector4(matrix.M31, matrix.M32, matrix.M34,0),
                new Vector4(matrix.M41, matrix.M42, matrix.M44,0),
                Vector4.UnitW
                );
            }
            //M14
            if (row == 1 && column == 4)
            {
                matrix = new Matrix(
                new Vector4(matrix.M21, matrix.M22, matrix.M23,0),
                new Vector4(matrix.M31, matrix.M32, matrix.M33,0),
                new Vector4(matrix.M41, matrix.M42, matrix.M43,0),
                Vector4.UnitW
                );
            }


            //M21
            if (row == 2 && column == 1)
            {
                matrix = new Matrix(
                new Vector4(matrix.M12, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M32, matrix.M33, matrix.M34, 0),
                new Vector4(matrix.M42, matrix.M43, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M22
            if (row == 2 && column == 2)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M31, matrix.M33, matrix.M34, 0),
                new Vector4(matrix.M41, matrix.M43, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M23
            if (row == 2 && column == 3)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M14, 0),
                new Vector4(matrix.M31, matrix.M32, matrix.M34, 0),
                new Vector4(matrix.M41, matrix.M42, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M24
            if (row == 2 && column == 4)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M13, 0),
                new Vector4(matrix.M31, matrix.M32, matrix.M33, 0),
                new Vector4(matrix.M41, matrix.M42, matrix.M43, 0),
                Vector4.UnitW
                );
            }

            //M31
            if (row == 3 && column == 1)
            {
                matrix = new Matrix(
                new Vector4(matrix.M12, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M22, matrix.M23, matrix.M24, 0),
                new Vector4(matrix.M42, matrix.M43, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M32
            if (row == 3 && column == 2)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M21, matrix.M23, matrix.M24, 0),
                new Vector4(matrix.M41, matrix.M43, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M33
            if (row == 3 && column == 3)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M14, 0),
                new Vector4(matrix.M21, matrix.M22, matrix.M24, 0),
                new Vector4(matrix.M41, matrix.M42, matrix.M44, 0),
                Vector4.UnitW
                );
            }
            //M34
            if (row == 3 && column == 4)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M13, 0),
                new Vector4(matrix.M21, matrix.M22, matrix.M23, 0),
                new Vector4(matrix.M41, matrix.M42, matrix.M43, 0),
                Vector4.UnitW
                );
            }

            //M41
            if (row == 4 && column == 1)
            {
                matrix = new Matrix(
                new Vector4(matrix.M12, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M22, matrix.M23, matrix.M24, 0),
                new Vector4(matrix.M32, matrix.M33, matrix.M34, 0),
                Vector4.UnitW
                );
            }
            //M42
            if (row == 4 && column == 2)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M13, matrix.M14, 0),
                new Vector4(matrix.M21, matrix.M23, matrix.M24, 0),
                new Vector4(matrix.M31, matrix.M33, matrix.M34, 0),
                Vector4.UnitW
                );
            }
            //M43
            if (row == 4 && column == 3)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M14, 0),
                new Vector4(matrix.M21, matrix.M22, matrix.M24, 0),
                new Vector4(matrix.M31, matrix.M32, matrix.M34, 0),
                Vector4.UnitW
                );
            }
            //M44
            if (row == 4 && column == 4)
            {
                matrix = new Matrix(
                new Vector4(matrix.M11, matrix.M12, matrix.M13, 0),
                new Vector4(matrix.M21, matrix.M22, matrix.M23, 0),
                new Vector4(matrix.M31, matrix.M32, matrix.M33, 0),
                Vector4.UnitW
                );
            }
            return matrix;
        }
        #endregion
        /// <summary>
        /// Provides a Matrix of Minors
        /// </summary>
        /// <param name="matrix">4x4 Matrix</param>
        /// <returns>returns the Matrix of Minors</returns>
        #region Matrix of Minors
        public static Matrix Minor(Matrix matrix)
        {
            Matrix tempmatrix =new Matrix();
            int z = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    tempmatrix[z] = CutMatrix(matrix, i, j).Determinant();
                    z++;
                }
            }
            return tempmatrix;
        }
        #endregion
        /// <summary>
        /// Retrieves the Cofactors of a Matrix 
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>returns the Cofactor Matrix</returns>
        #region Cofactors
        public static Matrix Cofactors(Matrix matrix)
        {
            Matrix tempmatrix = new Matrix();
            int z = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    tempmatrix[z] =  (float)Math.Pow(-1,i+j) * CutMatrix(matrix, i, j).Determinant();

                    z++;
                }
            }
            return tempmatrix;
        }
#endregion
        /// <summary>
        /// Finds the Adjoint of a Matrix
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>returns the adjoint of the matrix</returns>
        #region Adjoint
        public static Matrix Adjugate(Matrix matrix)
        {
            return Matrix.Transpose(Cofactors(matrix));
        }
        #endregion
        /// <summary>
        /// Finds the Inverse matrix using the Cofactor
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>returns the Inverse of the Matrix</returns>
        #region Inverse Matrix
        public static Matrix Inverse(Matrix matrix)
        {
            Matrix tempmatrix = new Matrix();
            tempmatrix = Matrix.Multiply(Adjugate(matrix), (1 / matrix.Determinant()));
            return tempmatrix;
        }
#endregion
    }
}
