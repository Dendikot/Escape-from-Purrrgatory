using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTilePrinter : MonoBehaviour
{

    private bool playerTilesPrinted = false;
    private bool enemyTilesPrinted = false;

    [SerializeField]
    private LayerMask collidableEnemies;
    [SerializeField]
    private LayerMask collidableObjects;

    [SerializeField]
    private Sprite playerPositionTile;
    [SerializeField]
    private Sprite playerMoveTile;
    [SerializeField]
    private Sprite enemyPositionTile;
    [SerializeField]
    private Sprite enemyMoveTile;


    public void PrintMovableTiles() {
        //Creates the Sprites and Objects for Walkable Tiles (For Mouse/Touch Based Movement)
        playerTilesPrinted = true;
        for (int nInd = 0; nInd < IsoGame.Access.GroupController.GetSprites.Length; nInd++) {

            //Back Left Character -> Left Direction
            if (IsoGame.Access.GroupController.GetSprites[nInd].sortingLayerName == "Back Left (1)") {                                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Left";
                positionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = playerPositionTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 11;
            
                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.left, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.left, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Left";
                    targetPositionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = playerMoveTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.left;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                    targetPositionTile.layer = 12;
                }
            }
            
            //Back Right Character -> Up Direction
            else if (IsoGame.Access.GroupController.GetSprites[nInd].sortingLayerName == "Back Right (2)") {                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Up";
                positionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = playerPositionTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 11;

                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.up, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.up, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Up";
                    targetPositionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = playerMoveTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.up;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                    targetPositionTile.layer = 12;                   
                }

            }

            //Front Right Character -> Right Direction
            else if (IsoGame.Access.GroupController.GetSprites[nInd].sortingLayerName == "Front Right (3)") {                                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Right";
                positionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = playerPositionTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 11;    

                // Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.right, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.right, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {                    
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Right";
                    targetPositionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = playerMoveTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.right;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                    targetPositionTile.layer = 12;                
                }

            }

            //Front Left Character -> Down Direction
            else if (IsoGame.Access.GroupController.GetSprites[nInd].sortingLayerName == "Front Left (4)") {                               
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Down";
                positionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = playerPositionTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);     
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 11;

                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.down, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.down, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Down";
                    targetPositionTile.transform.parent = IsoGame.Access.GroupController.GetSprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = playerMoveTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.down;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                    targetPositionTile.layer = 12;
                }
            }                         
        }
    }

    public void DestroyMovableTiles() {
        for (int nInd = 0; nInd < IsoGame.Access.GroupController.GetSprites.Length; nInd++) {
            while (IsoGame.Access.GroupController.GetSprites[nInd].transform.childCount > 0) {
                DestroyImmediate(IsoGame.Access.GroupController.GetSprites[nInd].transform.GetChild(0).gameObject);
            }
        }
        playerTilesPrinted = false;
    }
 
    public void PrintCollisionTiles(EnemyDummy enemy) {
    
        GameObject enemyCollisionTile = new GameObject();
        enemyCollisionTile.transform.parent = enemy.transform;
        enemyCollisionTile.name = enemyCollisionTile.transform.parent.name + "_Collision_Tile";
        enemyCollisionTile.AddComponent<SpriteRenderer>();
        enemyCollisionTile.GetComponent<SpriteRenderer>().sprite = enemyPositionTile;
        enemyCollisionTile.transform.localPosition = new Vector3(0,0,0);
        enemyCollisionTile.AddComponent<PolygonCollider2D>();
        enemyCollisionTile.gameObject.layer = 8;
        if (enemy.transform.gameObject.GetComponent<NeutralEnemy>() != null) {
            enemyCollisionTile.gameObject.layer = 9;
        }
    
    }

    public void DestroyCollisionTiles(EnemyDummy enemy) {
        if (enemy.transform.childCount > 0) {
           DestroyImmediate(enemy.transform.GetChild(0).gameObject);
        }
    }


    public bool CheckCollision(Vector3 direction, LayerMask layer)
    {
        Collider2D Collider;

        for (int nInd = 0; nInd < IsoGame.Access.GroupController.GetSprites.Length; nInd++)
        {
            Collider = Physics2D.OverlapPoint(IsoGame.Access.GroupController.GetSprites[nInd].transform.position + direction, layer);

            if (Collider != null)
            {
                return true;
            }
        }
        return false;
    }
}
