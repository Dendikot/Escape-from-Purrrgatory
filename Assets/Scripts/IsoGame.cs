using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoGame : MonoBehaviour
{
    private static IsoGame _instance;

    public static IsoGame Access { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Initialize();
    }
    
    private void Initialize()
    {
        m_Directions = new DirectionsModel(m_Grid);
    }
    
    // Components/Fields==========================================================================================
    [SerializeField]
    private Grid m_Grid;
    public Grid Grid { get { return m_Grid; } }

    [SerializeField]
    private float m_CommonLerpSpeed;
    public float LerpSpeed { get { return m_CommonLerpSpeed; } }

    private DirectionsModel m_Directions;
    public DirectionsModel Directions
    {
        get { return m_Directions; }
    }

    private List<IEnemyDummy> m_CurrentEnemies = new List<IEnemyDummy>();
    public List<IEnemyDummy> CurrentEnemeis { get { return m_CurrentEnemies; } set { m_CurrentEnemies = value; } }

    // Classes=====================================================================================================

    [SerializeField]
    private TurnController m_TurnController;
    public TurnController TurnController { get { return m_TurnController; } set { m_TurnController = value; } }
    //Do we Still need this one?
    //[SerializeField]
    //private IsometricCharacterMovement m_PlayerMovement;
    //public IsometricCharacterMovement PlayerMovement
    //{
    //    get { return m_PlayerMovement; }
    //}

    [SerializeField]
    private CharacterGroupController m_GroupController;
    public CharacterGroupController GroupController {
        get { return m_GroupController; }
    }


    //[SerializeField]
    //private CombatManager m_CombatManager;
    //public CombatManager CombatManager {
    //    get { return m_CombatManager; }
    //}

    //[SerializeField]
    //private EnemyManager m_EnemyManager;
    //public EnemyManager EnemyManager {
    //    get { return m_EnemyManager; }
    //}

}