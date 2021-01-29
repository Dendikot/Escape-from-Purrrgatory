using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : PlayerCombat
{


    void Awake()
    {
        stats = new Stats(attackValue, healthValue);
        groupController = IsoGame.Access.GroupController;
    }

    public override IEnumerator Attack() {

        Collider2D enemy = groupController.GetCollider(gameObject.transform, 1);

        if(enemy != null) {
            enemy.gameObject.GetComponent<IEnemyDummy>().ReceiveDamage(stats.Attack);
            stats.Attack -= 1;
            Debug.Log(stats.Attack);
        }

        yield return null;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Attack());
        }
    }
}
