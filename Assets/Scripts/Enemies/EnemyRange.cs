using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : IEnemyDummy
{

    void Start() {
        AddToList();
    }    
    
    override public IEnumerator Move()
    {
        Collider2D playerCollider = null;

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                Attack(playerCollider);
                yield break;
            }
        }



        int I = 10;
        Debug.Log("Didn't find the Player");
        while (I > 0)
        {
            I--;
        }

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                Attack(playerCollider);
                yield break;
            }
        }

        yield return null;
    }

    override public void ReceiveDamage(int damage)  
    {
        stats.Health -= damage;
        anim.SetTrigger("GotHit");
        if (stats.Health <= 0) {
            Die();   
        }        
    }

}
