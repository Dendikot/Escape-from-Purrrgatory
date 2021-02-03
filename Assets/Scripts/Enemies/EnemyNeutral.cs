using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNeutral : IEnemyDummy
{

    private bool m_isActive;
    public bool IsActive { set { m_isActive = value; } }

    [SerializeField]
    private GameObject neutralEnemyPrefab;
    private GameObject neutralEnemyState;


    override public IEnumerator Move()
    {
        if (m_isActive) {

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
        if (m_isActive == false) {
            m_isActive = true;
            neutralEnemyState = Instantiate(neutralEnemyPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
            neutralEnemyState.GetComponent<EnemyNeutral>().IsActive = true;
            neutralEnemyState.GetComponent<EnemyNeutral>().AddToList();
            Destroy(this.gameObject);
        }
        else {
            stats.Health -= damage;
            anim.SetTrigger("GotHit");
            if (stats.Health <= 0) {
                Die();   
            }
        } 
    }
}