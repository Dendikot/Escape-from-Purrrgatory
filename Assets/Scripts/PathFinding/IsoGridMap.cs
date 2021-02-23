using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoGridMap : MonoBehaviour
{
    public Node[,] GridMap;

    [SerializeField]
    private int m_X, m_Y;

    private DirectionsModel m_DirectionsModel;

    [SerializeField]
    private Grid m_Grid;

    [SerializeField]
    private LayerMask m_LayerMask;

    [SerializeField]
    private Transform m_TestPos;

    /// <summary>
    /// Actual path, 0 is the first step of the path, not the start position
    /// </summary>
    public List<Node> path;

    [SerializeField]
    private bool m_ShowGrid = false;

    private void Awake()
    {
        m_DirectionsModel = IsoGame.Access.Directions;//new OldPath.DirectionsModel(m_Grid);
        InstantiateGridMap();
    }

    public void InstantiateGridMap()
    {
        GridMap = new Node[m_X, m_Y];

        int LowBorderX = m_X / 2;
        int LowBorderY = m_Y / 2;

        Vector3 worldBeggining = transform.position
            + m_DirectionsModel.up / 2 + m_DirectionsModel.left / 2
            + m_DirectionsModel.left * LowBorderX - m_DirectionsModel.down * LowBorderY;

        for (int x = 0; x < m_X; x++)
        {
            for (int y = 0; y < m_Y; y++)
            {
                Vector3 pos = worldBeggining + x * m_DirectionsModel.right + y * m_DirectionsModel.down;
                Collider2D collision = Physics2D.OverlapPoint(pos, m_LayerMask);
                //Collider2D collision = Physics2D.OverlapCircle(pos, IsoGame.Access.Grid.cellSize.x / 2, m_LayerMask);
                bool walkable = true;
                if (collision != null)
                {
                    walkable = false;
                }

                GridMap[x, y] = new Node(walkable, pos, x, y);
            }
        }
    }

    public Node NodeFromPos(Vector3 pos)
    {
        if (GridMap != null)
        {
            foreach (Node node in GridMap)
            {
                float distance = Vector3.Distance(pos, node.position);

                if (distance <= 0.5)
                {
                    return node;
                }
            }
        }


        return null;
    }
    //align
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        //check onyl the straight neighbours

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0 || x == -1 && y == -1 || x == 1 && y == 1 || x == 1 && y == -1 || x == -1 && y == 1)
                    continue;

                int checkX = node.x + x;
                int checkY = node.y + y;

                if (checkX >= 0 && checkX < m_X && checkY >= 0 && checkY < m_Y)
                {
                    neighbours.Add(GridMap[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    private void OnDrawGizmos()
    {
        if (GridMap == null || m_ShowGrid == false)
        {
            return;
        }
        foreach (Node cellNode in GridMap)
        {
            //if (cellNode.x == Tx && cellNode.y == Ty)
            Gizmos.color = cellNode.walkable ? Color.white : Color.red;

            if (path != null)
                if (path.Contains(cellNode))
                    Gizmos.color = Color.black;

            Gizmos.DrawCube(cellNode.position, new Vector3(m_Grid.cellSize.x / 2, m_Grid.cellSize.y / 2, 0));
        }
    }
}
