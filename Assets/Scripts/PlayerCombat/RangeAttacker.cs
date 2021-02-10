using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker : PlayerCombat {

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject projectileParent;

    void Awake()
    {
        base.Awake();
    }

    public override IEnumerator Attack() {

        int range = 0;
        Collider2D enemy = null;

        //I hope this is an okay solution but feel free to suggest a better one if you figure something)        

        while(range <= 3) {
            enemy = groupController.GetCollider(gameObject.transform, range, collidableEnemies);

            if(enemy != null) {
                yield return StartCoroutine(SendProjectile(enemy.transform));
            }            
            range += 1;
        }
        stats.Attack -= 1;
        yield return null;

    }

    private IEnumerator SendProjectile(Transform enemy) {
        float elapsedTime = 0f;

        
        Transform projTransform = Instantiate(projectile, projectileParent.transform.position, Quaternion.identity).GetComponent<Transform>();

        Vector3 originalPosition = projectileParent.transform.position;
        Vector3 targetPosition = enemy.position + new Vector3 (0,0.25f,0);

        while (elapsedTime < 0.2) //this 0.2f might be interchangable
        {
            projTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(projTransform.gameObject);


        enemy.gameObject.GetComponent<EnemyDummy>().ReceiveDamage(stats.Attack);
        yield return null;
    } 

}
