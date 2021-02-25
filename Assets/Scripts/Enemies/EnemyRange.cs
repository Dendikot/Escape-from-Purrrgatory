using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyDummy
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    private GameObject projectileParent;

    //Only needed for the if Cat is Active Logic maybe theres a smarter way
    //no, definitely there's a smarter way ahaha 
    //Eh, I'll do it like this for now
    private CharacterGroupController m_GroupController;

    void Start() {
        AddToList();
        m_GroupController = IsoGame.Access.GroupController;
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
        Collider2D playerCollider = null;

        if(m_GroupController.CatIsActive == false /* && is not in Interest Range */) {
            for(int nInd = 0; nInd < m_Directions.directionsArr.Length; nInd++) {
                for (int rangeInd = 0; rangeInd <= 3; rangeInd++) {
                    playerCollider = Physics2D.OverlapPoint(this.transform.position + (m_Directions.directionsArr[nInd] * rangeInd), m_GroupController.collidableObjects);
                    if (playerCollider != null && playerCollider != this.gameObject.GetComponent<Collider2D>()) {
                        playerCollider.gameObject.GetComponent<Animator>().SetTrigger("GotHit");
                        yield return StartCoroutine(SendProjectile(playerCollider.transform));
                        yield break;
                    }
                }         
            }

        }

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                StartCoroutine(SendProjectile(playerCollider.transform));
                Attack(playerCollider);                
                yield break;
            }
        }

        yield return StartCoroutine(base.MoveToDir());

        for (int i = 0; i <= 3; i++) {
            playerCollider = GetPlayerCollider(gameObject.transform, i);
            if (playerCollider != null) {
                StartCoroutine(SendProjectile(playerCollider.transform));
                Attack(playerCollider);  
                yield break;
            }
        }
    }

    private IEnumerator SendProjectile(Transform player) {
        float elapsedTime = 0f;

        
        Transform projTransform = Instantiate(projectile, projectileParent.transform.position, Quaternion.identity).GetComponent<Transform>();

        Vector3 originalPosition = projectileParent.transform.position;
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
        audioSources[0].Play();
        if (stats.Health <= 0) {
            Die();   
        }        
    }

}
