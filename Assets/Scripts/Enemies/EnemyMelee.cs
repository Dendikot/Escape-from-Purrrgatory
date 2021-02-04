using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyDummy
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

        StartCoroutine(base.MoveToDir());

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
        audioSources[0].Play();
        if (stats.Health <= 0) {
            Die();   
        }
    }
}