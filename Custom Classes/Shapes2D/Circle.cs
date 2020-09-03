using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Custom_Classes.Shapes2D
{
    public struct Circle
    {
        public Point Position;
        private float radius;
        public float Radius {
            get {
                if(radius == 0)
                {
                    return 1;
                }
                return radius;
            }
            set {
                radius = value;
            }

        }
        public Circle(Point center,float radius)
        {
            this.Position = center;
            this.radius = radius;
        }
        public override string ToString()
        {
            return String.Format("r: {0}, pos: {1}",this.Radius,this.Position);
        }
        
    }
}
