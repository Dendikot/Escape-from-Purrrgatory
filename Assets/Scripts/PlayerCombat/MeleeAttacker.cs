using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : PlayerCombat
{


    void Awake()
    {
        base.Awake();
    }

    public override IEnumerator Attack() {

        Collider2D enemy = groupController.GetCollider(gameObject.transform, 1, collidableEnemies);

        if(enemy != null) {
            enemy.gameObject.GetComponent<EnemyDummy>().ReceiveDamage(stats.Attack);
        }

        stats.Attack -= 1;
        yield return null;
    }

}
