using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar_Grid : MonoBehaviour
{
    // flags for drawing grid
    public bool drawGrid;

    // the layer containing all obstacles
    public LayerMask unwalkableMask;

    // area that the Grid will cover
    public Vector2 gridWorldSize;

    // how much space a Node covers
    public float nodeRadius;
    private float nodeDiameter;

    // 2D array of Nodes, representing the Grid
    private Node[,] grid;
    private int gridSizeX, gridSizeY;

    private void Awake()
    {
        // figure out how many nodes can fit in the level
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        createGrid();
    }

    private void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = new Vector3(transform.position.x - gridWorldSize.x / 2, transform.position.y - gridWorldSize.y / 2, transform.position.z);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                bool walkable = !Physics2D.CircleCast(worldPoint, nodeRadius-0.01f, Vector2.zero, 0f, unwalkableMask);

                grid[x, y] = new Node(walkable, worldPoint, x, y);

            }
        }
    }

    // translare world position to Node index in grid
    public Node nodeFromWorldPoint(Vector3 _worldPosition)
    {
        float percentX = (_worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (_worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    // find neighbours of the given Node
    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    // draw the grid in Unity Editor
    private void OnDrawGizmos()
    {
        // draw the grid's boundaries
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 5));

        // draw the grid nodes
        if (drawGrid && grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = n.walkable ? Color.green : Color.red;
                Gizmos.DrawWireCube(n.worldPosition, new Vector3(nodeDiameter, nodeDiameter, nodeDiameter));
            }
        }
    }
}
