using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetRendererSorter : MonoBehaviour
{
    //this is Lena, it's my code don't delete pls it's the best code
    [SerializeField]
    private int m_sortingOrderBase = 0;
    public int SortingOrderBase { get {return m_sortingOrderBase; } set { m_sortingOrderBase = value; }}
    [SerializeField]
    private int m_offset = 0;
    public int Offset { set { m_offset = value; }}

    private Renderer myRenderer;
   
    private void Awake() {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(m_sortingOrderBase - (transform.parent.position.y - transform.position.y) - m_offset);
    }
}
