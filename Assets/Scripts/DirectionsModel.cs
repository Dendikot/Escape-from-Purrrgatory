using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DirectionsModel
{
    public DirectionsModel(Grid grid)
    {
        this.grid = grid;
        up = new Vector3(0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0);
        down = new Vector3(-0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0);
        left = new Vector3(-0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0);
        right = new Vector3(0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0);
    }

    private Grid grid;

    public Vector3 up;
    public Vector3 down;
    public Vector3 left;
    public Vector3 right;
}
