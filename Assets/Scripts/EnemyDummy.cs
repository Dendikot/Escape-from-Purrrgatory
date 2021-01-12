using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour, IEnemy
{
    [SerializeField]
    private int m_Health = 100;
    public int Health { get { return m_Health; } set { m_Health = value; } }

    [SerializeField]
    private Sprite collisionTile;

    void Awake() {
        PrintCollisionTiles();
    }

    private void PrintCollisionTiles() {
        GameObject enemyCollisionTile = new GameObject();
        enemyCollisionTile.transform.parent = gameObject.transform;
        enemyCollisionTile.name = enemyCollisionTile.transform.parent.name + "_Collision_Tile";
        enemyCollisionTile.AddComponent<SpriteRenderer>();
        enemyCollisionTile.GetComponent<SpriteRenderer>().sprite = collisionTile;
        enemyCollisionTile.transform.localPosition = new Vector3(0,0.45f,0);
        enemyCollisionTile.AddComponent<PolygonCollider2D>();
        enemyCollisionTile.gameObject.layer = 8;
    }

}
