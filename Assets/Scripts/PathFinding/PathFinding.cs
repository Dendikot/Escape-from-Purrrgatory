using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    //public Transform seeker, target;
    IsoGridMap m_IsoGridMap;

    Transform m_PlayerPos;

    DirectionsModel m_DirectionsModel;

    //[SerializeField]
    //Transform n1, n2;

    private void Awake()
    {
        m_IsoGridMap = GetComponent<IsoGridMap>();
        m_PlayerPos = IsoGame.Access.Player;
        m_DirectionsModel = IsoGame.Access.Directions;
    }

    //find a path between two test seekers



    private void Update()
    {
        //FindPath(seeker.position, target.position);
        //soooo
        //make it possible for enemy to access it 
        //realise enemy movement
        //GetDirectionEnemy(test.position);
        //FindPath(n1.position,n2.position);
    }

    public Vector3 GetDirectionEnemy(Vector3 pos)
    {
        FindPath(pos, m_PlayerPos.position);


        Debug.Log("Get a call");
        if (m_IsoGridMap.path == null || m_IsoGridMap.path.Count <= 0)
        {

            Debug.Log("Path is null");
            return transform.position;
        }

        //convert into directions
        //m_IsoGridMap.path[0].position;

        Debug.Log(m_IsoGridMap.path[0].position);
        Vector3 dir = pos - m_IsoGridMap.path[0].position;

        for (int nInd = 0; nInd < 4; nInd++)
        {
            float dist = Vector3.Distance(dir, m_DirectionsModel.directionsArr[nInd]);
            if (dist <= 0.5)
            {
                return m_DirectionsModel.directionsArr[nInd];
            }
        }

        m_IsoGridMap.InstantiateGridMap();

        return m_DirectionsModel.directionsArr[0];
    }


    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = m_IsoGridMap.NodeFromPos(startPos);
        Node targetNode = m_IsoGridMap.NodeFromPos(targetPos);

        if (startNode == null || targetNode == null)
        {
            return;
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in m_IsoGridMap.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        m_IsoGridMap.path = path;

    }

    private int GetDistance(Node A, Node B)
    {
        if (A == null || B == null)
        {
            return default;
        }

        int dstX = Mathf.Abs(A.x - B.x);
        int dstY = Mathf.Abs(A.y - B.y);

        if (dstX > dstY)
        {
            return 10 * dstY + 10 * (dstX - dstY);
        }
        return 10 * dstX + 10 * (dstY - dstX);
    }
}