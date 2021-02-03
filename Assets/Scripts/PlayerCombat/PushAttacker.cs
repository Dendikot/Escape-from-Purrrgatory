using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAttacker : PlayerCombat
{
    private int pushedTiles;

    void Awake()
    {
        base.Awake();
    }

    public override IEnumerator Attack() {

        pushedTiles = 0;
        Collider2D enemy = groupController.GetCollider(gameObject.transform, 1, collidableEnemies);

        if(enemy != null) {
            enemy.gameObject.GetComponent<IEnemyDummy>().ReceiveDamage(stats.Attack);
            StartCoroutine(PushEnemy(enemy.transform, gameObject.transform.position));
        }
        stats.Attack -= 1;        
        yield return null;

    }

    //DirectionVar is to calculate in what Direction the enemy is pushed
    private IEnumerator PushEnemy(Transform enemy, Vector3 directionVar) {
        if (enemy != null) {
            float elapsedTime = 0f;
            Vector3 originalPosition = enemy.position;
            Vector3 targetPosition = originalPosition + (originalPosition - (directionVar));


            //If any Collider is between Enemy and the position to be pushed at, it will end 
            if (Physics2D.OverlapPoint(targetPosition) != null) {
                yield break;
            }        


            //now the only Issue with this is, that you can't move to the former original Position right after attacking
            //Maybe we can fix together
            //Maybe we can trigger CheckMovableTiles() after attacking 
            //Maybe we can trigger CheckMovableTiles() after each move in TurnController
            //Need to trigger all Attacks from somewhere at the same time, and then add this to Turnbased Logic <-- Done


            while (elapsedTime < 0.2f) //this 0.2f might be interchangable
            {
                //THis is neccessary because of issues with Neutral Enemy transform being changed
                if (enemy == null) {
                    yield break;
                }
                enemy.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemy.position = targetPosition;

            pushedTiles++;

            if(pushedTiles < 2 ) {
                StartCoroutine(PushEnemy(enemy, originalPosition));
            }
        }
        yield return null;

    }
}
