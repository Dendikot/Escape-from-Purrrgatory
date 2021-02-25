using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put a bool here
public class CharacterGroupController : MonoBehaviour
{
    [SerializeField]
    public LayerMask collidableObjects;
    [SerializeField]
    public LayerMask collidableEnemies;
    [SerializeField]
    public LayerMask collidablePowerUps;

    private bool m_CatIsActive = false;
    public bool CatIsActive {get { return m_CatIsActive; } set { m_CatIsActive = value; } }

    [SerializeField]
    private GameObject m_CatStatusBox;

    [SerializeField]
    private Transform cat;

    [SerializeField]
    private AudioSource[] m_audioSources;
    public AudioSource[] AudioSources { get { return m_audioSources; } }


    [SerializeField]
    private Transform[] m_Characters;
    public Transform[] GetCharacters { get { return m_Characters; } }

    private Vector3[] Positions;
    public Vector3[] GetPositions { get { return Positions; } }

    [SerializeField]
    private GameObject[] m_PossibleTiles;

    //Only needed for the "no cat" Logic
    [SerializeField]
    private GameObject[] m_BluePositionTiles;

    private int subInd = 0;

    private DirectionsModel m_Directions;

    private bool m_PlayerTurn = true;
    public bool PlayerTurn {get {return m_PlayerTurn;} set { m_PlayerTurn = value; } }

    private TurnController m_TurnController;

    private DialogTriggerController m_DialogController;

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
        m_DialogController = IsoGame.Access.DialogController;
        m_TurnController = IsoGame.Access.TurnController;
        m_Directions = IsoGame.Access.Directions;
        Positions = new Vector3[m_Characters.Length];
        //m_SpriteStates = new bool[m_Characters.Length];

        for (int nInd = 0; nInd < m_Characters.Length; nInd++)
        {
            Positions[nInd] = m_Characters[nInd].localPosition;
        }
    }

    private void Start()
    {
        CheckMovableTiles();
        Rotate(0);
        ToggleCat();
    }

    private void Update()
    {
        Controll();
    }

    private void Controll()
    {
        if (m_PlayerTurn)
        {
            //Rotation();
            Movement();
        }
    }

/*
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
*/
    public void ToggleCat() {
        if(!m_CatIsActive) {
            DisableBlueTile(m_Characters[0]);
            m_CatStatusBox.SetActive(false);
        } 
        else {
            DisableBlueTile(m_Characters[0]);
            cat.position = m_Characters[0].position;
            m_Characters[0].gameObject.SetActive(false);
            m_Characters[0] = cat;
            m_Characters[0].gameObject.SetActive(true);
            Rotate(0);
            m_CatStatusBox.SetActive(true);
        }
    }

    //For Button Logic
    public void RotateLeft() {
        if (m_PlayerTurn) {
            Rotate(-1);
            m_audioSources[1].Play();
            m_TurnController.CountMove();

            if(m_CatIsActive == false) {
                DisableBlueTile(m_Characters[0]);
            }            
        }
    }

    public void RotateRight() {
        if (m_PlayerTurn) {
            Rotate(1);
            m_audioSources[2].Play();
            m_TurnController.CountMove();
            
            if(m_CatIsActive == false) {
                DisableBlueTile(m_Characters[0]);
            }
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
            //SpriteRenderer sprite = m_Characters[nInd].GetComponent<SpriteRenderer>();
            //sprite.sortingOrder = finInd;
            LayerCharacters(m_Characters[nInd], finInd);

            if (finInd == 0 || finInd == 1)
            {
                //sprite.flipX = !m_SpriteStates[nInd];

                //These Set original Rotation for the Position...
                m_Characters[finInd].rotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                //sprite.flipX = m_SpriteStates[nInd];

                //These Set original Rotation for the Position...
                m_Characters[finInd].rotation = Quaternion.Euler(0,180,0);
            }
            //These at all times rotate the Badger and the Fox
            if (finInd == 2 ||finInd == 3) {
                m_Characters[finInd].Rotate(0,180,0);
            }

            //This one is just black magic man
            if (nInd == 2|| nInd == 3) {
                m_Characters[finInd].Rotate(0,180,0);
            }
        }
    }

    private void LayerCharacters(Transform character, int layer) { 

        if (layer == 2 || layer == 3) {
            character.GetComponent<PositionRendererSorter>().SortingOrderBase = 1;
        }

        character.GetComponent<PositionRendererSorter>().Layer();

        foreach (Transform child in character) {
            OffsetRendererSorter offset = child.GetComponent<OffsetRendererSorter>();

            if (offset != null) {
                if(character.GetComponent<PushAttacker>() != null) {
                    if (layer == 0 || layer == 1) {
                        offset.Offset = 0;
                    } else offset.Offset = -3;
                }
                offset.Layer();
            }
        }

        //Ignore this Weird Stuff down there 
        
        /*
        if (layer == 0 || layer == 1) {
            character.GetComponent<PositionRendererSorter>().Offset = -1;
        } else character.GetComponent<PositionRendererSorter>().Offset = -2;
            

        foreach (Transform child in character) {
            if (child.GetComponent<SpriteRenderer>() != null) {
                child.GetComponent<SpriteRenderer>().sortingOrder = layer;

                //OffsetRenderer is for sorting different Body Parts. It sets sortingOrder of the Renderer = SortingOrderBase - offset
                //Offset is Set for the badger depending on position
                if(child.GetComponent<OffsetRendererSorter>() != null) {
                    child.GetComponent<OffsetRendererSorter>().SortingOrderBase = layer;
                    
                    //This is legit just for the badgers fucking shield
                    if (layer == 0 && character.GetComponent<PushAttacker>() != null || layer == 1 && character.GetComponent<PushAttacker>() != null) {
                        child.GetComponent<OffsetRendererSorter>().Offset = 1;
                    } else child.GetComponent<OffsetRendererSorter>().Offset = -2; 
                 //&& character.GetComponent<PushAttacker>() != null 
                }
            }
        }
        */
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

        foreach(Transform child in this.transform) {
            if(child.GetComponent<PlayerCombat>() != null) {
                child.GetComponent<PlayerCombat>().GetAnim.SetTrigger("Running");
                child.GetComponent<PlayerCombat>().FindPowerUps();
                m_DialogController.CheckForTrigger(child);
            }
        }

        m_isMoving = false;

        
        
        m_TurnController.CountMove();
        Rotate(0);

        m_audioSources[0].Play();

        CheckMovableTiles();
    }

    private void DisableBlueTile(Transform character) {
        foreach (GameObject blueTile in m_BluePositionTiles) {
            if (Vector3.Distance(blueTile.transform.position, character.position) < 0.001f) {
                blueTile.SetActive(false);
            } else blueTile.SetActive(true);
        }
        if (m_CatIsActive) {
            foreach (GameObject blueTile in m_BluePositionTiles) {
                blueTile.SetActive(true);
            }
        }
    }

    public void CheckMovableTiles()
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
    public Collider2D GetCollider(Transform character, int range, LayerMask layer) {
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

        Collider = Physics2D.OverlapPoint(character.position + direction, layer);

        if (Collider != null)
        {
            return Collider;
        }
        return null;
    }
}