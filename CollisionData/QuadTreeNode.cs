using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.CollisionData
{
    public class QuadTreeNode
    {
        protected List<QuadTreeNode> children = new List<QuadTreeNode>();
        protected List<QuadTreeData> contents = new List<QuadTreeData>();
        protected int currentDepth;
        protected static int maxDepth = 5;
        protected static int maxObjectsPerNode = 15;
        protected Rectangle nodeBounds;
        public QuadTreeNode(Rectangle bounds)
        {
            this.nodeBounds = bounds;
        }
        protected bool IsLeaf()
        {
            return children.Count == 0;
        }
        protected int NumObjects()
        {
            Reset();
            int objectCount = contents.Count;
            for (int i = 0; i < contents.Count; i++)
            {
                contents[i].flag = true;
            }
            List<QuadTreeNode> process = new List<QuadTreeNode>();
            process.Add(this);
            while(process.Count > 0)
            {
                QuadTreeNode processing = process[process.Count-1];
                if (!processing.IsLeaf())
                {
                    for (int i = 0; i < processing.children.Count; i++)
                    {
                        process.Add(processing.children[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < processing.contents.Count; i++)
                    {
                        if (!processing.contents[i].flag)
                        {
                            objectCount += 1;
                            processing.contents[i].flag = true;
                        }
                    }
                }
                process.RemoveAt(0);
            }
            Reset();
            return objectCount;
        }
        /// <summary>
        /// Insert object into a new node
        /// </summary>
        /// <param name="data"></param>
        public void Insert(QuadTreeData data)
        {
            if (!Collisions.RectangleRectangle(data.bounds, nodeBounds))
            {
                return;
            }
            if(IsLeaf() && contents.Count + 1 > maxObjectsPerNode)
            {
                Split();
            }
            if (IsLeaf())
            {
                contents.Add(data);
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Insert(data);
                }
            }
        }
        /// <summary>
        /// Removes childnodes
        /// </summary>
        /// <param name="data"></param>
        protected void Remove(QuadTreeData data)
        {
            if (IsLeaf())
            {
                int removeIndex = -1;
                for (int i = 0; i < contents.Count; i++)
                {
                    if (contents[i].obj == data.obj)
                    {
                        removeIndex = i;
                        break;
                    }
                }
                if (removeIndex != -1)
                {
                    contents.RemoveAt(1);
                } 
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Remove(data);
                }
            }
            Shake();
        }
        /// <summary>
        /// The Update function needs to be called whenever an object moves. This function will remove the object from the tree, and reinsert the object.
        /// </summary>
        /// <param name="data"></param>
        public void Update(QuadTreeData data)
        {
            Remove(data);
            Insert(data);
        }
        /// <summary>
        /// if the total number of objects within a node (and all of its children) is less than the maximum number of objects permitted per node the current child nodes are eliminated, and the node becomes a leaf.
        /// </summary>
        protected void Shake()
        {
            if (!IsLeaf())
            {
                int numObjects = NumObjects();
                if (numObjects == 0)
                {
                    children.Clear();
                }
                else if (numObjects < maxObjectsPerNode)
                {
                    List<QuadTreeNode> process = new List<QuadTreeNode>();
                    process.Add(this);
                    while(process.Count() > 0)
                    {
                        QuadTreeNode processing = process[process.Count - 1];
                        if (!processing.IsLeaf())
                        {
                            for (int i = 0; i < processing.children.Count; i++)
                            {
                                process.Add(processing.children[i]);
                            }
                        }
                        else
                        {
                            contents.InsertRange(contents.Count - 1, processing.contents);
                        }
                        process.RemoveAt(0);
                    }
                    children.Clear();
                }
            }
        }
        /// <summary>
        /// The Split helper function splits the current node into four child nodes, and then inserts all the objects the current node has into its new children.
        /// </summary>
        protected void Split()
        {
            if (currentDepth + 1 >= maxDepth)
            {
                return;
            }

            Vector2 min, max;
            min.X = nodeBounds.Location.X;
            min.Y = nodeBounds.Location.Y;
            max.X = nodeBounds.Location.X + nodeBounds.Width;
            max.Y = nodeBounds.Location.Y + nodeBounds.Height;
            Vector2 center = min + ((max - min) * 0.5f);

            Rectangle[] childAreas =
            {
                FromMinMax(new Vector2(min.X,min.Y),new Vector2(center.X,center.Y)),
                FromMinMax(new Vector2(center.X,min.Y),new Vector2(max.X,center.Y)),
                FromMinMax(new Vector2(center.X,center.Y),new Vector2(max.X,max.Y)),
                FromMinMax(new Vector2(min.X,center.Y), new Vector2(center.X,max.Y))
            };
            for (int i = 0; i < 4; i++)
            {
                children.Add(new QuadTreeNode(childAreas[i]));
                children[i].currentDepth = currentDepth + 1;
            }
            for (int i = 0; i < contents.Count; i++)
            {
                children[i].Insert(contents[i]);
            }
            contents.Clear();
        }
        /// <summary>
        /// Resets the QuadTree
        /// </summary>
        protected void Reset()
        {
            if (IsLeaf())
            {
                for (int i = 0; i < contents.Count; i++)
                {
                    contents[i].flag = false;
                }
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Reset();
                }
            }
        }
        private Rectangle FromMinMax(Vector2 min, Vector2 max)
        {
            Point pmin, pmax;
            pmin.X = (int)min.X;
            pmin.Y = (int)min.Y;
            pmax.X = (int)max.X;
            pmax.Y = (int)max.Y;

            return new Rectangle(pmin, pmax - pmin);
        }
        /// <summary>
        /// Gives a list of Objects that are nearby enough for collision
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<QuadTreeData> Query(Rectangle area)
        {
            List<QuadTreeData> result = new List<QuadTreeData>();
            if (!Collisions.RectangleRectangle(area,nodeBounds))
            {
                return result;
            }
            if (IsLeaf())
            {
                for (int i = 0; i < contents.Count; i++)
                {
                    if (Collisions.RectangleRectangle(contents[i].bounds,area))
                    {
                        result.Add(contents[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    List<QuadTreeData> recurse = children[i].Query(area);
                    if(recurse.Count > 0)
                    {
                        result.InsertRange(result.Count-1,recurse);
                    }
                }
            }
            return result;
        }
    }
}
