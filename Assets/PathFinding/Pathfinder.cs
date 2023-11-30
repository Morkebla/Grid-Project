using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMaster;

namespace PathFinding
{
    public class Pathfinder
    {
        GridMaster.Grid grid;
        public PathNode startPosition;
        public PathNode endPosition;

        public List<PathNode> FindPath()
        {
            GridMaster.Grid grid = GridMaster.Grid.GetInstance();
            return FindPathActual(startPosition, endPosition);
        }
        private List <PathNode> FindPathActual(PathNode start, PathNode target)
        {
            List<PathNode> foundPath = new List<PathNode>();

            List <PathNode> openSet = new List<PathNode> ();
            HashSet<PathNode> closedSet = new HashSet<PathNode>();

            openSet.Add(start);

            while(openSet.Count > 0)
            {
                PathNode currentNode = openSet[0];

                for (int i = 0; i < openSet.Count; i++)
                {
                    if(openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                    {
                        if (currentNode.Equals(openSet[i]))
                        {
                            currentNode = openSet[i];
                        }
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode.Equals(target))
                {
                    foundPath = RetracePath(start, currentNode);
                    break;
                }

                foreach(PathNode neighbour in GetNeighbours(currentNode))
                {
                    if (!closedSet.Contains(neighbour))
                    {
                        float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                        if(newMovementCostToNeighbour< neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, target);

                            neighbour.parentNode = currentNode;

                            if (!openSet.Contains(neighbour))
                            {
                                openSet.Add(neighbour);
                            }
                        }
                    }
                }
            }
            return foundPath;
        }

        private int GetDistance(PathNode posA, PathNode posB)
        {
            int distX = Mathf.Abs(posA.x - posB.x);
            int distY = Mathf.Abs(posA.y - posB.y);

            if(distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }
            return 14 * distX + 10 * (distY - distX);
        }

        private List<PathNode> GetNeighbours(PathNode node)
        {
            List<PathNode> retList = new List<PathNode> ();

            for (int x = -1; x <= 1; x++)
            {
                for( int y = -1; y <= 1; y++)
                {
                        if (x == 0 && y == 0)
                        {

                        }
                        else
                        {
                            PathNode searchPos = new PathNode();

                            searchPos.x = node.x + x;
                            searchPos.y = node.y + y;

                            PathNode newNode = GetNeighbourNode(searchPos, node);
                            
                            if(newNode != null)
                        {
                            retList.Add(newNode);
                        }
                        } 
                }
            }
            return retList;
        }

        private PathNode GetNeighbourNode(PathNode adjPos,PathNode currentNodePos)
        {
            PathNode retVal = null;

            PathNode node = GridMaster.Grid.GetInstance().GetNode(adjPos.x, adjPos.y);

            if (node != null && node.isWalkable)
            {
                retVal = node;
            }
            int originalX = adjPos.x - currentNodePos.x;
            int originalY = adjPos.y - currentNodePos.y;
            
            if(Mathf.Abs(originalX)==1 && Mathf.Abs(originalY) == 1)
            {
                PathNode neighbour1 = GridMaster.Grid.GetInstance().GetNode(currentNodePos.x + originalX,currentNodePos.y);
                if(neighbour1 != null && !neighbour1.isWalkable)
                {
                    retVal = null;
                }
                PathNode neighbour2 = GridMaster.Grid.GetInstance().GetNode(currentNodePos.x, currentNodePos.y + originalY);
                if(neighbour2 != null && !neighbour2.isWalkable)    
                {
                    retVal = null;
                }
            }
            return retVal;
        }
        private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            PathNode currentNode = endNode;
            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }
            path.Reverse();
            return path;
        }
    }
}
