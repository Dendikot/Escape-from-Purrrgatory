using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    //this is Lena, it's my code don't delete pls it's the best code
    [SerializeField]
    private int m_sortingOrderBase = 0;
    public int SortingOrderBase { get {return m_sortingOrderBase; } set { m_sortingOrderBase = value; }}
    [SerializeField]
    private int m_offset = 0;
    public int Offset { set { m_offset = value; }}

    [SerializeField]
    private bool isObject;

    private List<Renderer> m_Renderer;
    private Transform characters;

    void Awake() {
        m_Renderer = new List<Renderer>();
        characters = IsoGame.Access.GroupController.transform;
        for (int i = 0; i < this.transform.childCount; i++) {
            if(this.transform.GetChild(i).GetComponent<Renderer>() != null) {
                m_Renderer.Add(this.transform.GetChild(i).GetComponent<Renderer>());

            }
        }
        Layer();
    }

    public void Layer()
    {
        for(int i = 0; i < m_Renderer.Count; i++) {
            if(isObject == false) {
                m_Renderer[i].sortingOrder = (int)(m_sortingOrderBase - transform.position.y - m_offset);
            }
            else if(isObject) {
                m_Renderer[i].sortingOrder = (int)(m_sortingOrderBase - (m_Renderer[i].transform.position.y) - m_offset);
            }
        }
    }

    public int GetLayer() {
        return (int)(m_sortingOrderBase - transform.position.y - m_offset);
    }
}
