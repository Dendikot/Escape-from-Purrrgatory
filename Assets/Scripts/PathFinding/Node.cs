using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x, y;
    public bool walkable;
    public Vector3 position;

    public int gCost;
    public int hCost;
    public Node parent;


    public Node(bool walkable, Vector3 worldPosition, int x, int y)
    {
        this.walkable = walkable;
        this.position = worldPosition;
        this.x = x;
        this.y = y;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}