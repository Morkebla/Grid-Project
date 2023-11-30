using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] int width = 16;
        [SerializeField] int depth = 16;

        [SerializeField] GameObject _tile;

        private int[,] gridArray;


        public Vector3 startNodePosition;
        public Vector3 endNodePosition;

        public PathNode[,] pathNodeGrid;

        public bool start;
        void Update()
        {
            if (start)
            {
                start = false;
                PathFinding.Pathfinder path = new PathFinding.Pathfinder();

                pathNodeGrid[1, 1].isWalkable = false;

                PathNode startNode = GetNodeFromVector3(startNodePosition);
                PathNode end = GetNodeFromVector3(endNodePosition);

                path.startPosition = startNode;
                path.endPosition = end;

                List<PathNode> pathList = path.FindPath();

                startNode.worldObject.SetActive(false);
                foreach (PathNode n in pathList)
                {
                    n.worldObject.SetActive(false);
                }
            }
        }

        public PathNode GetNode(int x, int y)
        {
            PathNode retVal = null;

            if (x < width && x >= 0 && y >= 0 && y < depth)
            {
                retVal = pathNodeGrid[x, y];
            }
            return retVal;
        }

        public PathNode GetNodeFromVector3(Vector3 pos)
        {
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            int z = Mathf.RoundToInt(pos.z);

            PathNode retVal = GetNode(x, y);
            return retVal;
        }

        //Singleton
        public static Grid instance;
        public static Grid GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gridArray = new int[width, depth];
            pathNodeGrid = new PathNode[width, depth];

            Vector3 tilePosition = Vector3.zero;

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                
                    tilePosition.x = x;
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    tilePosition.z = y;
                    GameObject tile = Instantiate(_tile, tilePosition, Quaternion.identity) as GameObject;

                    //set the name to be the coordinates.
                    tile.transform.name = x.ToString() + " " + y.ToString();
                    // parent the nodes under grid to be more organised.
                    tile.transform.parent = transform;

                    PathNode pathNode = new PathNode();
                    pathNode.x = x;
                    pathNode.y = y;
                    pathNode.worldObject = tile;

                    pathNodeGrid[x, y] = pathNode;
                } 
            }
        }
    }
}