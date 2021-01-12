using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{


    [SerializeField]
    private Sprite collisionTile;
    [SerializeField]
    private Stats m_Stats;

    private GameObject enemyCollisionTile;

    public Stats Stats { get { return m_Stats;} }

    void Awake() {
        PrintCollisionTiles();
        m_Stats.Health = 100;
        m_Stats.Attack = 5;
    }

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
