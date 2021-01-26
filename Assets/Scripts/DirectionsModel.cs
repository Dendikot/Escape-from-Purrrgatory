using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DirectionsModel
{
    public DirectionsModel(Grid grid)
    {
        directionsArr = new Vector3[4];
        this.grid = grid;
        //up
        directionsArr[0] = new Vector3(0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0);
        up = directionsArr[0];

        //down
        directionsArr[1] = new Vector3(-0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0);
        down = directionsArr[1];

        //left
        directionsArr[2] = new Vector3(-0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0);
        left = directionsArr[2];

        //right
        directionsArr[3] = new Vector3(0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0);
        right = directionsArr[3];
    }

    private Grid grid;

    public Vector3 up;
    public Vector3 down;
    public Vector3 left;
    public Vector3 right;

    public Vector3[] directionsArr;
    
}
