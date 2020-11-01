using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Enemy nearestEnemy; //<- I will Add some logic to this eventually, for testing i just use SerializeField

    private bool isMoving;  //<- probably won't need this depending on our Input and the Turnbased System
    private Vector3 originalPosition, targetPosition;
    private float timeToMove = 0.2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving && CheckMovePosition(grid.WorldToCell(transform.position) + new Vector3Int(1, 0, 0)))
        {
            StartCoroutine(MovePlayer(new Vector3(0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0))); //Use 1/2 Cell Size here depending on the move direction, maybe theres a inherit grid solution

        }

        if (Input.GetKey(KeyCode.A) && !isMoving && CheckMovePosition(grid.WorldToCell(transform.position) + new Vector3Int(0, 1, 0)))
        {
            StartCoroutine(MovePlayer(new Vector3(-0.5f * grid.cellSize.x, 0.5f * grid.cellSize.y, 0)));
        }

        if (Input.GetKey(KeyCode.S) && !isMoving && CheckMovePosition(grid.WorldToCell(transform.position) + new Vector3Int(-1, 0, 0)))
        {
            StartCoroutine(MovePlayer(new Vector3(-0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0)));
        }

        if (Input.GetKey(KeyCode.D) && !isMoving && CheckMovePosition(grid.WorldToCell(transform.position) + new Vector3Int(0, -1, 0)))
        {
            StartCoroutine(MovePlayer(new Vector3(0.5f * grid.cellSize.x, -0.5f * grid.cellSize.y, 0)));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0f;

        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        GetPlayerPosition(grid.WorldToCell(transform.position));
        //GetGameObjectOnTile(new Vector3Int(0, 0, 0), grid.WorldToCell(transform.position));

       nearestEnemy.GetEnemyPosition();

        isMoving = false;
    }

    public bool CheckMovePosition(Vector3Int movePosition)
    {
        for(int i = 0; i < GameModel.instance.enemies.Length; i++)
        {
            if (movePosition == GameModel.instance.enemies[i].GetEnemyPosition())
            {
                Debug.Log("There's already an Enemy!");
                return false;
            }
        }
        return true;
    }

    public void GetPlayerPosition(Vector3Int position)
    {
        Debug.Log("Player is on: " + tilemap.GetTile(position).name + " & World Position: " + position);
    }

    /* I might come back to this later
    public void GetGameObjectOnTile(Vector3Int position, Vector3Int playerPosition)
    {
        if (tilemap.GetInstantiatedObject(position) != null)
        {
            Debug.Log("On 0/0/0 is : " + tilemap.GetInstantiatedObject(position).name + " & and on Players Position is: " + tilemap.GetInstantiatedObject(playerPosition).name);
        }
        else Debug.Log("On 0/0/0 is : null & and on Players Position is: " + tilemap.GetInstantiatedObject(playerPosition).ToString());
    }
    */
}
