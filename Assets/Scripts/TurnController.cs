using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Button m_TurnButton;

    private void Awake()
    {
        m_Enemies = IsoGame.Access.CurrentEnemeis;
        m_GroupController = IsoGame.Access.GroupController;
    }

    public void CountMove()
    {
        m_PlayerMoves++;

        for (int nInd = 0; nInd < m_Enemies.Count; nInd++)
        {
            m_Enemies[nInd].UpdateEnemyMoveTile();
        }

        if (m_PlayerMoves == 2)
        {
            //Fades Movement Tiles
            FadeTiles(m_MoveTiles, new Color(0.4f, 0.4f, 0.4f, 0.4f));  

            m_GroupController.PlayerTurn = false;
            if (m_Enemies.Count <= 0) {
                m_GroupController.PlayerTurn = true;

                FadeTiles(m_MoveTiles, new Color(1, 1, 1, 1));     

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
            m_Enemies[nInd].DisableEnemyMoveTile();
            yield return StartCoroutine(m_Enemies[nInd].Move());
        }
        m_PlayerMoves = 0;
        IsoGame.Access.GroupController.PlayerTurn = true;
        m_MoveTiles.gameObject.SetActive(true);

        //Un-Fades Movement Tiles
        FadeTiles(m_MoveTiles, new Color(1, 1, 1, 1));
        for (int nInd = 0; nInd < m_Enemies.Count; nInd++)
        {
            m_Enemies[nInd].UpdateEnemyMoveTile();
        }
        m_TurnButton.interactable = true;
        m_GroupController.PlayerTurn = true;
        m_EnemyTurn = false;
    }

    public void EndTurn() {
        m_TurnButton.interactable = false;
        m_GroupController.PlayerTurn = false;
        m_EnemyTurn = true;
        StartCoroutine(EnemiesMove());
    }

    private void FadeTiles(Transform moveTiles, Color c) {
        foreach (Transform child in m_MoveTiles) {
            child.GetComponent<SpriteRenderer>().color = c;
        }        
    }
}