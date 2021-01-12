﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{


    [SerializeField]
    private Sprite collisionTile;
    [SerializeField]
    private Stats m_Stats;

    
    [SerializeField]
    private LayerMask m_CollidablePlayers;
    public LayerMask CollidablePlayers { get { return m_CollidablePlayers; } }
    [SerializeField]
    private LayerMask m_CollidableObjects;
    public LayerMask CollidableObjects { get { return m_CollidableObjects; } }

    private GameObject enemyCollisionTile;

    public Stats Stats { get { return m_Stats;} }


    public void PrintCollisionTiles() {
        enemyCollisionTile = new GameObject();
        enemyCollisionTile.transform.parent = gameObject.transform;
        enemyCollisionTile.name = enemyCollisionTile.transform.parent.name + "_Collision_Tile";
        enemyCollisionTile.AddComponent<SpriteRenderer>();
        enemyCollisionTile.GetComponent<SpriteRenderer>().sprite = collisionTile;
        enemyCollisionTile.transform.localPosition = new Vector3(0,0,0);
        enemyCollisionTile.AddComponent<PolygonCollider2D>();
        enemyCollisionTile.gameObject.layer = 8;
    }

    public void DestroyCollisionTiles() {
        DestroyImmediate(enemyCollisionTile);
}

}
