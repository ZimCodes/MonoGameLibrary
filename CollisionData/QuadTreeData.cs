using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.CollisionData
{
    public class QuadTreeData
    {
        public object obj;
        public Rectangle bounds;
        public bool flag;
        
        public QuadTreeData(object obj= null, Rectangle b = new Rectangle())
        {
            this.obj = obj;
            this.bounds = b;
            this.flag = false;
        }
    }
}
