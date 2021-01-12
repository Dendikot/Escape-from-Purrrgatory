using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCat : MonoBehaviour
{
    // This Script holds all important Abilities and the Stats linked to the Cat

    [SerializeField]
    private Stats m_Stats;
    public Stats Stats { get { return m_Stats; } }

    void Awake() {
        m_Stats.Health = 10;
        m_Stats.Attack = 0;
    }
}
