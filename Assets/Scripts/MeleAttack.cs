using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // CheckCollider();
        // Maybe create player management
        Collider2D col = CheckCollider( IsoGame.Access.Directions.left);
        if   (col != null)
        {
            col.GetComponent<IEnemy>().Health--;
        }
    }    

    private Collider2D CheckCollider(Vector3 posToCheck)
    {
        Collider2D col = Physics2D.OverlapPoint(transform.position + posToCheck);
        if (col != null)
        {
            Debug.Log("Found him " + col.name);
            return col;
        }

        return null;
    }
}
