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
    public SpriteRenderer[] GetSprites { get { return Sprites; } }
    
    private List<string> LayerIndex;

    private int subInd = 0;

    private bool m_isMoving = false;

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
            IsoGame.Access.TilePrinter.DestroyMovableTiles();
            Rotate(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsoGame.Access.TilePrinter.DestroyMovableTiles();
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
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Down")
            {
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Left")
            {
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));              
            }
            if (hit.collider != null && hit.collider.name == "Target Tile Direction Right")
            {
                IsoGame.Access.TilePrinter.DestroyMovableTiles();
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

        IsoGame.Access.TilePrinter.PrintMovableTiles();
        

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


        IsoGame.Access.TilePrinter.PrintMovableTiles();


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

    
}