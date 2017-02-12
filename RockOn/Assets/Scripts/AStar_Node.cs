using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // is the Node walkable, or is it an obstacle
    public bool walkable;

    // position of this Node in the world
    public Vector3 worldPosition;

    // distance from start and target Nodes
    public int gCost, hCost;

    // index in the grid
    public int gridX, gridY;

    // previous Node in the path
    public Node parent;

    // constructor for the Node
    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }
}
