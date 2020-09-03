using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util
{
    public class CMP
    {
        public CMP(){}
        /// <summary>
        /// Equal to comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is equal to right side</returns>
        public static bool IsEq(float left, float right)
        {
            return  left == right;
        }
        /// <summary>
        /// Not Equal to comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is not equal to right side</returns>
        public static bool IsNotEq(float left, float right)
        {
            return (left != right);
        }
        /// <summary>
        /// Greater than Equal to comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is greater than equal to right side</returns>
        public static bool IsGreaterEq(float left, float right)
        {
            return (left >= right);
        }
        /// <summary>
        /// Lesser than Equal to comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is less than equal to right side</returns>
        public static bool IsLesserEq(float left, float right)
        {
            return (left <= right);
        }
        /// <summary>
        /// Greater than comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is greater than right side</returns>
        public static bool IsGreater(float left, float right)
        {
            return (left > right);
        }
        /// <summary>
        /// Lesser than comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>returns a bool if left side is less than right side</returns>
        public static bool IsLesser(float left, float right)
        {
            return (left < right);
        }
    }
    
}
