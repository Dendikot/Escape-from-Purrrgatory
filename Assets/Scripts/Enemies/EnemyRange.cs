using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyDummy
{
    [SerializeField]
    GameObject projectile;

    void Start() {
        AddToList();
    }    
    
    override public IEnumerator Move()
    {
        Collider2D playerCollider = null;

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                StartCoroutine(SendProjectile(playerCollider.transform));
                Attack(playerCollider);                
                yield break;
            }
        }

        StartCoroutine(base.MoveToDir());

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                StartCoroutine(SendProjectile(playerCollider.transform));
                Attack(playerCollider);  
                yield break;
            }
        }

        yield return null;
    }

    private IEnumerator SendProjectile(Transform player) {
        float elapsedTime = 0f;

        
        Transform projTransform = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Transform>();

        Vector3 originalPosition = this.transform.position;
        Vector3 targetPosition = player.position;

        while (elapsedTime < 0.2) //this 0.2f might be interchangable
        {
            projTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(projTransform.gameObject);

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
