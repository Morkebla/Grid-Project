using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{
    public class PathNode
    {
        public int x;
        public int y;

        public float hCost;
        public float gCost;

        public float fCost
        {
            get { return gCost + hCost; }
        }

        public PathNode parentNode;
        public bool isWalkable = true;

        public GameObject worldObject;

        public Nodetype nodetype;
        public enum Nodetype
        {
            ground,
            air,
        }
    }
}