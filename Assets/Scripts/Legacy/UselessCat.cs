using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCat : MonoBehaviour
{
    // This Script holds all important Abilities and the Stats linked to the Cat

    private Stats m_Stats;
    public Stats Stats { get { return m_Stats; } set { m_Stats = value; } }

    [SerializeField]
    private int attackValue = 0;
    [SerializeField]
    private int healthValue = 10;
    
    void Awake() {
        m_Stats = new Stats (attackValue, healthValue);
    }
}
