using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker : PlayerCombat {

    Stats stats;

    void Awake()
    {
        stats = new Stats(attackValue, healthValue);
        groupController = IsoGame.Access.GroupController;
    }

    public override IEnumerator Attack() {

        float range = 0;
        Collider2D enemy = null;

        //I hope this is an okay solution but feel free to suggest a better one if you figure something)
        while(range <= 3.0f) {
            enemy = groupController.GetCollider(gameObject.transform, range);
            if(enemy != null) {
                enemy.gameObject.GetComponent<IEnemyDummy>().ReceiveDamage(stats.Attack);
                stats.Attack -= 1;
                yield break;
            }            
            range += 0.1f;
        }

        yield return null;

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Attack());
        }
    }
}
