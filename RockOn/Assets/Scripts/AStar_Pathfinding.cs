using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AStar_Pathfinding : MonoBehaviour
{
    public AStar_Grid grid; // grid of Nodes that represent walkable space
    public AStar_PathRequestManager manager; // script managing pathfinding requests from enemies
    private int maxStepCount = 30; // to stop searching for path if it takes too many steps

    public void startFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(findPath(startPos, targetPos));
    }

    private IEnumerator findPath(Vector3 startPos, Vector3 targetPos)
    {
        // path and flag for PathManager
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        // find the Nodes at given positions
        Node startNode = grid.nodeFromWorldPoint(startPos);
        Node targetNode = grid.nodeFromWorldPoint(targetPos);

        // if target node is not walkable, find it's neighbours that are
        if (!targetNode.walkable)
        {
            List<Node> ns = grid.getNeighbours(targetNode);
            foreach (Node n in ns)
            {
                if (n.walkable)
                {
                    targetNode = n;
                    break;
                }
            }
        }

        // if target node is still unwalkable, skip finding path
        if (targetNode.walkable)
        {
            // the set of Nodes to be evaluated
            List<Node> openSet = new List<Node>();

            // the set of Nodes already evaluated
            HashSet<Node> closedSet = new HashSet<Node>();

            // add the start Node to openSet
            openSet.Add(startNode);

            // count how many iterations pathfinding took
            int steps = 0;

            // look for a path
            while (openSet.Count > 0)
            {
                // stop searching for path after max number of steps
                if (++steps > maxStepCount) break;

                // current Node is the Node in openSet with the lowest fCost (or hCost if fCosts are equal)
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                // remove the current Node from openSet and add it to closedSet
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // if current Node is the target, we found the path
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                // for each neighbour of the current Node
                foreach (Node neighbour in grid.getNeighbours(currentNode))
                {
                    // if neighbour is not traversable or neighbour is in closedSet skip to the next neighbour
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                    // cost of the path
                    int newMoveCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);

                    // if new path to neighbour is shorter or neighbour is not in openSet
                    if (newMoveCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        // set fCost of neighbour
                        neighbour.gCost = newMoveCostToNeighbour;
                        neighbour.hCost = getDistance(neighbour, targetNode);

                        // set parent of neighbour
                        neighbour.parent = currentNode;

                        // if neighbour is not in openSet add neigbour to openSet
                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                    }
                }
            }
        }
        yield return null;

        // if path was found, save it to array
        if (pathSuccess) waypoints = retracePath(startNode, targetNode);

        // give manager the path and flag
        manager.finishedProcessingPath(waypoints, pathSuccess);
    }

    // make a list of nodes in the found path
    private Vector3[] retracePath(Node startNode, Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path.ToArray();
    }

    // calculate distance (cost) between 2 Nodes
    private int getDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        // vertical and horizontal cost is 10, diagonal cost is 14
        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        else return 14 * dstX + 10 * (dstY - dstX);
    }
}
