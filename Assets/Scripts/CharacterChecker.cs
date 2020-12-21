using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChecker : MonoBehaviour
{
    //check if works, then you can shift everything to the controller
    public bool CheckCollidableObject(Vector3 posToCheck, LayerMask mask)
    {
        posToCheck += transform.position;
        
        Collider2D Collider = Physics2D.OverlapPoint(posToCheck, mask);
        //Physics2D.OverlapCollider()
        if (Collider != null)
        {
            Debug.Log("Found him " + Collider.name);
            return false;
        }
        return true;
    }
}
