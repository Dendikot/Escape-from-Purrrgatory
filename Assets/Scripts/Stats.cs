using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Stat Build", menuName = "Stats/Stat Build", order = 1)]
public class Stats
{

    private int m_Attack;
    private int m_Health;

    private int m_AttackMaxValue;
    private int m_HealthMaxValue;

    private StatusBar m_healthBar;
    private StatusBar m_forceBar;


    public Stats(int attack, int health) {
        m_AttackMaxValue = attack;
        m_HealthMaxValue = health;

        m_Attack = m_AttackMaxValue;
        m_Health = m_HealthMaxValue;
    }
    
    public int MaxAttack { get { return m_AttackMaxValue; } }
    public int Attack 
    {
        get { return m_Attack; } 
        set { 
            m_Attack = value; m_Attack = Mathf.Clamp(m_Attack, 0, m_AttackMaxValue); 
            if(m_forceBar != null) {
                m_forceBar.StatChange(m_Attack, m_AttackMaxValue); }
            }
    }

    public int MaxHealth { get { return m_HealthMaxValue; } }
    public int Health 
    {
        get { return m_Health; } 
        set { m_Health = value; m_Health = Mathf.Clamp(m_Health, 0, m_HealthMaxValue); 
        if(m_healthBar != null) {
            m_healthBar.StatChange(m_Health, m_HealthMaxValue); }
        }
    }

    public StatusBar HealthBar { set { m_healthBar = value; } }
    public StatusBar ForceBar { set { m_forceBar = value; } }    
}
