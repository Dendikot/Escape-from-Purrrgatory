using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{

    [SerializeField]
    private LayerMask m_CollidablePlayers;
    public LayerMask CollidablePlayers {
        get { return m_CollidablePlayers; }
    }
    [SerializeField]
    private LayerMask m_CollidableEnemies;
    public LayerMask CollidableEnemies {
        get { return m_CollidableEnemies; }
    }    
    [SerializeField]
    private LayerMask m_CollidableObjects;
    public LayerMask CollidableObjects {
        get { return m_CollidableObjects; }
    }
    
    private Stats m_Stats;
    public Stats Stats { get { return m_Stats; } set { m_Stats = value; } }

}
