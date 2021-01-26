using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private int m_PlayerMoves = 0;

    private bool m_EnemyMove;
    public bool EnemyMove { get { return m_EnemyMove; } }

    //might be a good idea to place it to iso game
    private List<IEnemyDummy> m_Enemies;

    private void Awake()
    {
        m_Enemies = IsoGame.Access.CurrentEnemeis;
    }

    public void CountMove()
    {
        m_PlayerMoves++;
        if (m_PlayerMoves == 2)
        {
            m_EnemyMove = false;
            StartCoroutine(EnemiesMove());
        }
    }

    
    private IEnumerator EnemiesMove()
    {
        Debug.Log("Enemies move");
        if (m_Enemies.Count <= 0)
        {
            Debug.Log("0 enemies");
            yield return null;
        }

        for (int nInd = 0; nInd < m_Enemies.Count; nInd++)
        {
            yield return StartCoroutine(m_Enemies[nInd].Move());
        }

        //rename
        m_EnemyMove = true;
        m_PlayerMoves = 0;
    }
}