using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Build", menuName = "Stats/Stat Build", order = 1)]
public class Stats : ScriptableObject
{
    [SerializeField]
    private int m_Attack;
    [SerializeField]
    private int m_Health;

    //I Use Getters and Setters even though i use methods for the logic so i can easily implement Mathf.Clamp
    public int Attack 
    {
        get { return m_Attack; } 
        set { m_Attack = value; m_Attack = Mathf.Clamp(m_Attack, 0, 999);}
    }

    public int Health 
    {
        get { return m_Health; } 
        set { m_Health = value; m_Health = Mathf.Clamp(m_Health, 0, 999); }
    }

    //Figured maybe writing the needed Combat Logic here so we can just call the Methods from the Turnbased, eg. player1.stats.ReduceHealthByAttack() 
    public void ReduceAttackByOne() 
    {
        Attack -= 1;
    }

    public void ReduceHealthByAttack(int damage) 
    {
        Health -= damage;
    }
}
