using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveCat : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField]
    private PlayerCombat[] characters;

    void Start() {
        foreach(Transform child in this.transform) {
            OffsetRendererSorter offset = child.GetComponent<OffsetRendererSorter>();

            if (offset != null) {
                offset.Layer();
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == this.gameObject.GetComponent<Collider2D>())
            {
                for (int i = 0; i < characters.Length; i++) {
                    characters[i].RecruitCat();
                 
                    if (IsoGame.Access.GroupController.CatIsActive == true) {
                        isActive = true;
                    }
                }
            }

            if (isActive) {
                DestroyImmediate(this.gameObject);
            }
        }
    }
}
