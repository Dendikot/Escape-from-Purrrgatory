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
            StartCoroutine(EnemiesMove());
        }
    }

    private IEnumerator WaitForEnemyTurn() {
        yield return new WaitForSeconds(0.5f);
        m_EnemyMove = false;
    }
    
    private IEnumerator EnemiesMove()
    {
        //Why Wait for Enemy Turn? --> If we go from players turn to enemy turn instantly, collision checking will be weird after rotating/moving
        //E.g. Crow rotates left, Badger stands in front of Enemy, Enemy will find Crow instead of badger if going to his turn instanly. By waiting, we workaround that
        //Plus, it gives some time for a turn change animation or something like that. Maybe it's a dumb idea tho
        yield return StartCoroutine(WaitForEnemyTurn());
        if (m_EnemyMove == false) {
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
}