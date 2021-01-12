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

    // Classes=====================================================================================================

    //Do we Still need this one?
    [SerializeField]
    private IsometricCharacterMovement m_PlayerMovement;
    public IsometricCharacterMovement PlayerMovement
    {
        get { return m_PlayerMovement; }
    }

    [SerializeField]
    private CharacterGroupController m_GroupController;
    public CharacterGroupController GroupController {
        get { return m_GroupController; }
    }

    [SerializeField]
    private CombatManager m_CombatManager;
    public CombatManager CombatManager {
        get { return m_CombatManager; }
    }

    [SerializeField]
    private TurnBasedBehaviour m_TurnBased;
    public TurnBasedBehaviour TurnBased {
        get { return m_TurnBased; }
    }
}