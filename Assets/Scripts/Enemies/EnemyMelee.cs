using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : IEnemyDummy
{
    void Start() {
        AddToList();
    }        
    
    override public IEnumerator Move()
    {

        Collider2D playerCollider = GetPlayerCollider(gameObject.transform, 1);        

        if(playerCollider != null) {
            Attack(playerCollider);
            yield break;
        }

        int I = 10;
        Debug.Log("Didn't find the Player");
        while (I > 0)
        {
            I--;
        }

        playerCollider = GetPlayerCollider(gameObject.transform, 1); 

        if (playerCollider != null) {
            Attack(playerCollider);
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