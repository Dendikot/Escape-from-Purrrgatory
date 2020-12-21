using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour, IEnemy
{
    [SerializeField]
    private int m_Health = 100;
    public int Health { get { return m_Health; } set { m_Health = value; } }
}
