using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNeutral : IEnemyDummy
{


    private bool isActive;
    [SerializeField]
    private Sprite activeSprite;

    void Update() {
        if(stats.Health == 0) {
            Die();
        }
    }

    override public IEnumerator Move()
    {
        if (isActive) {

            Collider2D playerCollider = GetPlayerCollider(gameObject.transform, 1);
            if (playerCollider != null) {
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

        }

        yield return null;
    }

    override public void ReceiveDamage(int damage) {
        if (isActive == false) {
            isActive = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite; 
            AddToList();          
        }
        else {
            stats.Health -= damage;
        } 
    }
}