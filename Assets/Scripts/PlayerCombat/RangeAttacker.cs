using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker : PlayerCombat {

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
                enemy.gameObject.GetComponent<IEnemyDummy>().ReceiveDamage(stats.Attack);
                yield return null;
            }            
            range += 1;
        }
        stats.Attack -= 1;
        yield return null;

    }

}
