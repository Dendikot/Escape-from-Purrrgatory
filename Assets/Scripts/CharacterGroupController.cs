using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put a bool here
public class CharacterGroupController : MonoBehaviour
{
    [SerializeField]
    private LayerMask collidableObjects;
    [SerializeField]
    public LayerMask collidableEnemies;

    [SerializeField]
    private Transform[] m_Characters;
    public Transform[] GetCharacters { get { return m_Characters; } }

    private Vector3[] Positions;
    public Vector3[] GetPositions { get { return Positions; } }

    [SerializeField]
    private GameObject[] m_PossibleTiles;

    private int subInd = 0;

    private DirectionsModel m_Directions;

    private bool m_PlayerTurn = true;
    public bool PlayerTurn { set { m_PlayerTurn = value; } }

    private TurnController m_TurnController;

    private int SubInd
    {
        get { return subInd; }
        set
        {
            if (value < 0)
            {
                value = 3;
            }
            else if (value >= m_Characters.Length)
            {
                value -= m_Characters.Length;
            }
            subInd = value;
        }
    }

    private int spriteSubInt = 0;
    private int _SpriteSubInt
    {
        get { return spriteSubInt; }
        set
        {
            if (value >= m_Characters.Length)
            {
                value = 0;
            }
            spriteSubInt = value;
        }
    }

    private bool m_isMoving = false;

    private bool[] m_SpriteStates;

    private void Awake()
    {
        m_TurnController = IsoGame.Access.TurnController;
        m_Directions = IsoGame.Access.Directions;
        Positions = new Vector3[m_Characters.Length];
        m_SpriteStates = new bool[m_Characters.Length];

        for (int nInd = 0; nInd < m_Characters.Length; nInd++)
        {
            Positions[nInd] = m_Characters[nInd].localPosition;
            m_SpriteStates[nInd] = m_Characters[nInd].GetComponent<SpriteRenderer>().flipX;
        }
    }

    private void Start()
    {
        CheckMovableTiles();
    }

    private void Update()
    {
        Controll();
    }

    private void Controll()
    {
        if (m_PlayerTurn)
        {
            Rotation();
            Movement();
        }
    }


    private void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
            m_TurnController.CountMove();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
            m_TurnController.CountMove();
        }
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0) && !m_isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == m_PossibleTiles[0])
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
            }
            if (hit.collider != null && hit.collider.gameObject == m_PossibleTiles[1])
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
            }
            if (hit.collider != null && hit.collider.gameObject == m_PossibleTiles[2])
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));
            }
            if (hit.collider != null && hit.collider.gameObject == m_PossibleTiles[3])
            {
                StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));
            }

            //with this, even if you click anywhere on the screen, it will count as a move. Maybe move this somewhere else
            m_TurnController.CountMove();
        }
    }

    //private void Movement()
    //{

    //Changed this to Mouse based Movement
    //if (Input.GetMouseButtonDown(0) && !m_isMoving)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

    //    if (hit.collider != null && hit.collider.name == "Target Tile Direction Up") 
    //    {
    //        m_TilePrinter.DestroyMovableTiles();
    //        StartCoroutine(MovePlayer(IsoGame.Access.Directions.up));
    //    }
    //    if (hit.collider != null && hit.collider.name == "Target Tile Direction Down")
    //    {
    //        m_TilePrinter.DestroyMovableTiles();
    //        StartCoroutine(MovePlayer(IsoGame.Access.Directions.down));
    //    }
    //    if (hit.collider != null && hit.collider.name == "Target Tile Direction Left")
    //    {
    //        m_TilePrinter.DestroyMovableTiles();
    //        StartCoroutine(MovePlayer(IsoGame.Access.Directions.left));              
    //    }
    //    if (hit.collider != null && hit.collider.name == "Target Tile Direction Right")
    //    {
    //        m_TilePrinter.DestroyMovableTiles();
    //        StartCoroutine(MovePlayer(IsoGame.Access.Directions.right));              
    //    }
    //}        
    //}

    private void Rotate(int value)
    {
        SubInd += value;

        for (int nInd = 0; nInd < m_Characters.Length; nInd++)
        {
            int finInd = IndexProcessor(SubInd, nInd);
            m_Characters[nInd].localPosition = Positions[finInd];
            SpriteRenderer sprite = m_Characters[nInd].GetComponent<SpriteRenderer>();
            sprite.sortingOrder = finInd;

            if (finInd == 3 || finInd == 0)
            {
                sprite.flipX = !m_SpriteStates[nInd];
            }
            else
            {
                sprite.flipX = m_SpriteStates[nInd];
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

        m_isMoving = false;

        //IsoGame.Access.TurnBased.IncreasePlayerActions();
        CheckMovableTiles();
    }

    private void CheckMovableTiles()
    {
        for (int nInd = 0; nInd < m_Directions.directionsArr.Length; nInd++)
        {
            if (CheckCollision(m_Directions.directionsArr[nInd], collidableObjects))
            {
                m_PossibleTiles[nInd].SetActive(false);
            }
            else
            {
                m_PossibleTiles[nInd].SetActive(true);
            }
        }
    }

    /*
     * Same logic is used in other scritps, same as direction
     * there might be a way to place it separately and reuse it, not nessesary for now though
     * */
    private bool CheckCollision(Vector3 direction, LayerMask layer)
    {
        Collider2D Collider;

        for (int nInd = 0; nInd < m_Characters.Length; nInd++)
        {
            Collider = Physics2D.OverlapPoint(m_Characters[nInd].position + direction, layer);

            if (Collider != null)
            {
                return true;
            }
        }
        return false;
    }

    //range is mostly and solely for ranged enemy. If you attack the tile in front of you, just put 1, eg. GetCollider(character, 1)
    public Collider2D GetCollider(Transform character, int range) {
        Collider2D Collider = null;
        Vector3 direction = new Vector3(0,0,0);
        int d = 4;

        for (int nInd = 0; nInd < m_Characters.Length; nInd++) {
            if(character == m_Characters[nInd]) {
                d = IndexProcessor(SubInd, nInd);
                //direction = m_Directions.directionsArr[d];
            }
        }

        switch(d) {
            case 0:
                direction = IsoGame.Access.Directions.left * range;
                break;
            case 1:
                direction = IsoGame.Access.Directions.up * range;
                break;
            case 2:
                direction = IsoGame.Access.Directions.right * range;
                break;
            case 3:
                direction = IsoGame.Access.Directions.down * range;
                break;
            case 4:
                break;
        }

        Collider = Physics2D.OverlapPoint(character.position + direction, collidableEnemies);

        if (Collider != null)
        {
            return Collider;
        }
        return null;
    }
}