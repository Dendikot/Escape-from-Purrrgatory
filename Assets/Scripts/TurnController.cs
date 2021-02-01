using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private int m_PlayerMoves = 0;

    //might be a good idea to place it to iso game
    private List<IEnemyDummy> m_Enemies;

    private bool m_EnemyTurn = false;
    public bool EnemyTurn { get { return m_EnemyTurn; } }

    private void Awake()
    {
        m_Enemies = IsoGame.Access.CurrentEnemeis;
    }

    public void CountMove()
    {
        m_PlayerMoves++;
        if (m_PlayerMoves == 2)
        {
            IsoGame.Access.GroupController.PlayerTurn = false;
        }
    }
    
    private IEnumerator EnemiesMove()
    {
        if (m_Enemies.Count <= 0)
        {
            Debug.Log("0 enemies");
            yield return null;
        }

        for (int nInd = 0; nInd < m_Enemies.Count; nInd++)
        {
            yield return StartCoroutine(m_Enemies[nInd].Move());
        }
        m_PlayerMoves = 0;
        IsoGame.Access.GroupController.PlayerTurn = true;
        m_EnemyTurn = false;
    }

    public void EndTurn() {
        m_EnemyTurn = true;
        StartCoroutine(EnemiesMove());
    }
}