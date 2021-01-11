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
    private Transform[] Characters;
    private Vector3[] Positions;

    private SpriteRenderer[] Sprites;
    private List<string> LayerIndex;

    private int subInd = 0;

    private bool m_isMoving = false;

    [SerializeField]
    private Sprite movableTile;

    [SerializeField]
    private Collider2D m_CollisionChecker;

    private enum State {
        PrintTiles,
        DestroyTiles,
        WaitForInput
    }

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

    private State state;

    private void Awake()
    {
        Positions = new Vector3[Characters.Length];
        Sprites = new SpriteRenderer[Characters.Length];
        LayerIndex = new List<string>();

        state = State.PrintTiles;

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
        Rotation();
        Movement();
    }

    private void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            state = State.DestroyTiles;
            PrintMovableTiles();
            Rotate(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            state = State.DestroyTiles;
            PrintMovableTiles();
            Rotate(-1);
        }
    }

    private void Movement()
    {

        PrintMovableTiles();
        //Changed this to Mouse based Movement
        if (Input.GetMouseButtonDown(0) && !m_isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.name == "Target Tile Direction Up") 
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
                state = State.DestroyTiles;
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Down")
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
                state = State.DestroyTiles;
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Left")
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));
                state = State.DestroyTiles;                
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Right")
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));
                state = State.DestroyTiles;                
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
        state = State.PrintTiles;
        FlipCharacters();
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
        if (CheckCollision(direction))
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

        m_isMoving = false;
        state = State.PrintTiles;
    }

    public bool CheckCollision(Vector3 direction)
    {
        Collider2D Collider;

        for (int nInd = 0; nInd < Characters.Length; nInd++)
        {
            Collider = Physics2D.OverlapPoint(Characters[nInd].position + direction, collidableObjects);
            if (Collider != null)
            {
                return true;
            }
        }
        return false;
    }

    private void PrintMovableTiles() {
        //Creates the Sprites and Objects for Walkable Tiles (For Mouse/Touch Based Movement)
        switch(state) {       
            case State.PrintTiles:
                for (int nInd = 0; nInd < Sprites.Length; nInd++) {
                    if (Sprites[nInd].sortingLayerName == "Back Left (1)") {
                        GameObject targetPositionTile = new GameObject();
                        targetPositionTile.name = "Target Tile Direction Left";
                        targetPositionTile.transform.parent = Sprites[nInd].transform;
                        targetPositionTile.AddComponent<SpriteRenderer>();
                        targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                        targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.left;
                        targetPositionTile.AddComponent<PolygonCollider2D>();
                    }
                    else if (Sprites[nInd].sortingLayerName == "Back Right (2)") {
                        GameObject targetPositionTile = new GameObject();
                        targetPositionTile.name = "Target Tile Direction Up";
                        targetPositionTile.transform.parent = Sprites[nInd].transform;
                        targetPositionTile.AddComponent<SpriteRenderer>();
                        targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                        targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.up;
                        targetPositionTile.AddComponent<PolygonCollider2D>();
                    }
                    else if (Sprites[nInd].sortingLayerName == "Front Right (3)") {
                        GameObject targetPositionTile = new GameObject();
                        targetPositionTile.name = "Target Tile Direction Right";
                        targetPositionTile.transform.parent = Sprites[nInd].transform;
                        targetPositionTile.AddComponent<SpriteRenderer>();
                        targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                        targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.right;
                        targetPositionTile.AddComponent<PolygonCollider2D>();
                    }
                    else if (Sprites[nInd].sortingLayerName == "Front Left (4)") {
                        GameObject targetPositionTile = new GameObject();
                        targetPositionTile.name = "Target Tile Direction Down";
                        targetPositionTile.transform.parent = Sprites[nInd].transform;
                        targetPositionTile.AddComponent<SpriteRenderer>();
                        targetPositionTile.GetComponent<SpriteRenderer>().sprite = movableTile;
                        targetPositionTile.transform.localPosition = new Vector3(0,0,0) + IsoGame.Access.Directions.down;
                        targetPositionTile.AddComponent<PolygonCollider2D>();
                    }                         
                }
                state = State.WaitForInput;
                break;
            case State.DestroyTiles: {
                for (int nInd = 0; nInd < Sprites.Length; nInd++) {
                    while (Sprites[nInd].transform.childCount > 0) {
                        DestroyImmediate(Sprites[nInd].transform.GetChild(0).gameObject);
                    }
                }
                state = State.WaitForInput;
                break;

            }
            case State.WaitForInput:
                break;
        }
    }
}