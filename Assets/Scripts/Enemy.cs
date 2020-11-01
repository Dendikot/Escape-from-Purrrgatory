using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tilemap tilemap;


    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3Int GetEnemyPosition()
    {
        return GameModel.instance.grid.WorldToCell(transform.position);
    }
}
