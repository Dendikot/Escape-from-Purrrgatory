using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyDummy
{
    private DialogTriggerController m_DialogController;

    void Start() {
        //AddToList(); //Remove with Interest Range Implementation and Move to Update
        m_DialogController = IsoGame.Access.DialogController;
    }

    void Update() {
        /*

        Maybe shouldnt be in update but somewhere where it's called regularly

        if('is in Interest Range' && IsoGame.Access.CurrentEnemeis.Contains(this) == false) {
            AddToList();
        } else if('is not in Interest Range' && IsoGame.Access.CurrentEnemeis.Contains(this)) {
            RemoveFromList();
        }
        */
    }

    override public IEnumerator Move()
    {

        Collider2D playerCollider = GetPlayerCollider(gameObject.transform, 1);        

        if(playerCollider != null) {
            Attack(playerCollider);
            yield break;
        }

        yield return StartCoroutine(base.MoveToDir());

        playerCollider = GetPlayerCollider(gameObject.transform, 1); 

        if (playerCollider != null) {
            Attack(playerCollider);
        }

    }

    override public void ReceiveDamage(int damage)  
    {
        stats.Health -= damage;
        anim.SetTrigger("GotHit");
        audioSources[0].Play();
        if (stats.Health <= 0) {
            Die();
            if(m_DialogController.onEnemyDefeated == false) {
                m_DialogController.SendMessage("EnemyDefeated");
                m_DialogController.onEnemyDefeated = true;
            }
        }
    }
}