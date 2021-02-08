using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private int m_PlayerMoves = 0;

    //might be a good idea to place it to iso game
    private List<EnemyDummy> m_Enemies;

    [SerializeField]
    private Transform m_MoveTiles;

    private bool m_EnemyTurn = false;
    public bool EnemyTurn { get { return m_EnemyTurn; } }

    private CharacterGroupController m_GroupController;

    private void Awake()
    {
        m_Enemies = IsoGame.Access.CurrentEnemeis;
        m_GroupController = IsoGame.Access.GroupController;
    }

    public void CountMove()
    {
        m_PlayerMoves++;
        if (m_PlayerMoves == 2)
        {
            //Fades Movement Tiles
            foreach (Transform child in m_MoveTiles) {
                Color c = Color.grey;
                c.a = 0.4f;
                child.GetComponent<SpriteRenderer>().color = c;
            }
            m_GroupController.PlayerTurn = false;
            if (m_Enemies.Count <= 0) {
                m_GroupController.PlayerTurn = true;
                foreach (Transform child in m_MoveTiles) {
                    Color c = Color.white;
                    c.a = 1.0f;
                    child.GetComponent<SpriteRenderer>().color = c;
                }      
                m_PlayerMoves = 0;            
            }
        }
    }
    
    private IEnumerator EnemiesMove()
    {
        m_MoveTiles.gameObject.SetActive(false);
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
        m_MoveTiles.gameObject.SetActive(true);

        //Un-Fades Movement Tiles
        foreach (Transform child in m_MoveTiles) {
            Color c = Color.white;
            c.a = 1.0f;
            child.GetComponent<SpriteRenderer>().color = c;
        }        
        m_EnemyTurn = false;
    }

    public void EndTurn() {
        m_EnemyTurn = true;
        StartCoroutine(EnemiesMove());
    }
}