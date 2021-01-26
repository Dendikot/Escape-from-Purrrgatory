using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTilesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_MovableTiles;

    private CharacterGroupController m_CharacterGroup;

    private void Awake()
    {
        m_CharacterGroup = GetComponent<CharacterGroupController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckMovableTile();
        }
    }

    public void TilesStatus(bool status)
    {
        for (int nInd = 0; nInd < m_MovableTiles.Length; nInd++)
        {
            m_MovableTiles[nInd].SetActive(status);
        }
    }

    public void UpdateTiles()
    {
        
    }

    private void CheckMovableTile(Transform position = null)
    {
        //so the story is
        //you need to chec one step ahead of each character

        Debug.Log(m_CharacterGroup.GetCharacters.Length);
    }
}
