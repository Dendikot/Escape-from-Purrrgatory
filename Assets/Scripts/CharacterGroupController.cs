using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroupController : MonoBehaviour
{
    [SerializeField]
    private IsometricCharacterMovement[] m_CharactersControllers;

    [SerializeField]
    private LayerMask collidableObjects;
    [SerializeField]
    private LayerMask collidableEnemies;

    [SerializeField]
    private Transform[] Characters;
    private Vector3[] Positions;

    public Transform[] GetCharacters { get { return Characters; } }

    private SpriteRenderer[] Sprites;
    private List<string> LayerIndex;

    private int subInd = 0;

    private bool movableTilesPrinted = false;

    private bool m_isMoving = false;

    [SerializeField]
    private Sprite movableTile;
    [SerializeField]
    private Sprite characterTile;

    [SerializeField]
    private Collider2D m_CollisionChecker;

    private enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }


    private int SubInd
    {
        get
        {
            return subInd;
        }
        set
        {
            if (value < 0)
            {
                value = 3;
            }
            else if (value >= Characters.Length)
            {
                value -= Characters.Length;
            }
            subInd = value;
        }
    }

    private void Awake()
    {
        Positions = new Vector3[Characters.Length];
        Sprites = new SpriteRenderer[Characters.Length];
        LayerIndex = new List<string>();

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Positions[nInd] = Characters[nInd].localPosition;
            Sprites[nInd] = Characters[nInd].GetComponent<SpriteRenderer>();
            LayerIndex.Add(Sprites[nInd].sortingLayerName);
        }
    }

    private void Update()
    {
        //check new input unity system once several consoles are confurmed
        if(IsoGame.Access.TurnBased.isPlayerTurn()) {
            Rotation();
            Movement();
        }
        //This will Later move to AI Movement
        if(IsoGame.Access.TurnBased.isEnemyTurn()) {
            EnemyTurn();
        }
    }

    private void EnemyTurn() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            IsoGame.Access.TurnBased.EndEnemyTurn();
        }
    }

    private void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyMovableTiles();
            Rotate(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyMovableTiles();
            Rotate(-1);
        }
    }

    private void Movement()
    {

        //Changed this to Mouse based Movement
        if (Input.GetMouseButtonDown(0) && !m_isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.name == "Target Tile Direction Up") 
            {
                DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Down")
            {
                DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Left")
            {
                DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));              
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Right")
            {
                DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));              
            }
        }        
    }

    private void Rotate(int value)
    {
        SubInd += value;

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Characters[nInd].localPosition = Positions[IndexProcessor(SubInd, nInd)];
            Sprites[nInd].sortingLayerName = LayerIndex[IndexProcessor(SubInd, nInd)];
        }

        if (movableTilesPrinted == false) {
            PrintMovableTiles();
        }

        FlipCharacters();
        IsoGame.Access.TurnBased.IncreasePlayerActions();
    }

    private void FlipCharacters()
    {
        for (int nInd = 0; nInd < Sprites.Length; nInd++)
        {
            if (Sprites[nInd].sortingLayerName == "Back Left (1)" || Sprites[nInd].sortingLayerName == "Front Left (4)")
            {
                Sprites[nInd].flipX = true;
            }
            else
            {
                Sprites[nInd].flipX = false;
            }
        }
    }

    /// <summary>
    /// Method to calculate final index for Positions array
    /// </summary>
    /// <param name="value1">SubInd</param>
    /// <param name="value2">nInd</param>
    /// <returns></returns>
    private int IndexProcessor(int value1, int value2)
    {
        int finalInd = value1 + value2;

        while (finalInd >= Positions.Length)
        {
            finalInd -= Positions.Length;
        }

        return finalInd;
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        if (CheckCollision(direction, collidableEnemies))
        {
            yield break;
        }
        m_isMoving = true;

        float elapsedTime = 0f;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + direction;

        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        if (movableTilesPrinted == false) {
            PrintMovableTiles();
        }

        m_isMoving = false;

        IsoGame.Access.TurnBased.IncreasePlayerActions();
    }

    public bool CheckCollision(Vector3 direction, LayerMask layer)
    {
        Collider2D Collider;

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Collider = Physics2D.OverlapPoint(Characters[nInd].position + direction, layer);

            if (Collider != null)
            {
                return true;
            }
        }
        return false;
    }

    //I might move all this shit to a seperate Script for optimization, depending on neccessity

    public void PrintMovableTiles() {
        //Creates the Sprites and Objects for Walkable Tiles (For Mouse/Touch Based Movement)
        movableTilesPrinted = true;
        for (int nInd = 0; nInd < Sprites.Length; nInd++) {

            //Back Left Character -> Left Direction
            if (Sprites[nInd].sortingLayerName == "Back Left (1)") {                                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Left";
                positionTile.transform.parent = Sprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = characterTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 9;
            
                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.left, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.left, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Left";
                    targetPositionTile.transform.parent = Sprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.left;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                }
            }
            
            //Back Right Character -> Up Direction
            else if (Sprites[nInd].sortingLayerName == "Back Right (2)") {                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Up";
                positionTile.transform.parent = Sprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = characterTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 9;

                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.up, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.up, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Up";
                    targetPositionTile.transform.parent = Sprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.up;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                }

            }

            //Front Right Character -> Right Direction
            else if (Sprites[nInd].sortingLayerName == "Front Right (3)") {                                
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Right";
                positionTile.transform.parent = Sprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = characterTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 9;    

                // Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.right, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.right, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {                    
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Right";
                    targetPositionTile.transform.parent = Sprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.right;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                }

            }

            //Front Left Character -> Down Direction
            else if (Sprites[nInd].sortingLayerName == "Front Left (4)") {                               
                //Blue Tiles
                GameObject positionTile = new GameObject();
                positionTile.name = "Position Tile Character Down";
                positionTile.transform.parent = Sprites[nInd].transform;
                positionTile.AddComponent<SpriteRenderer>();
                positionTile.GetComponent<SpriteRenderer>().sprite = characterTile;
                positionTile.transform.localPosition = new Vector3(0,0,0);     
                positionTile.AddComponent<PolygonCollider2D>();
                positionTile.layer = 9;

                //Green Tiles
                if (CheckCollision(IsoGame.Access.Directions.down, collidableObjects) == false && CheckCollision(IsoGame.Access.Directions.down, collidableEnemies) == false && IsoGame.Access.TurnBased.isPlayerTurn()) {
                    GameObject targetPositionTile = new GameObject();
                    targetPositionTile.name = "Target Tile Direction Down";
                    targetPositionTile.transform.parent = Sprites[nInd].transform;
                    targetPositionTile.AddComponent<SpriteRenderer>();
                    targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                    targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.down;
                    targetPositionTile.AddComponent<PolygonCollider2D>();
                }
            }                         
        }
    }

    public void DestroyMovableTiles() {
        for (int nInd = 0; nInd < Sprites.Length; nInd++) {
            while (Sprites[nInd].transform.childCount > 0) {
                DestroyImmediate(Sprites[nInd].transform.GetChild(0).gameObject);
            }
        }
        movableTilesPrinted = false;
    }
    
}