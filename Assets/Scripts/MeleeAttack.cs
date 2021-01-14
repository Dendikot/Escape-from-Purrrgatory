﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField]
    private LayerMask collidableNeutralEnemies;
    [SerializeField]
    private LayerMask collidableEnemies;

    private Stats m_Stats;
    public Stats Stats { get { return m_Stats; } set { m_Stats = value; } }

    [SerializeField]
    private int attackValue = 10;
    [SerializeField]
    private int healthValue = 30;    

    void Awake() {
        m_Stats = new Stats (attackValue, healthValue);
    }

    public void Attack()
    {
        Collider2D col = null;

            if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Left (1)") {
                col = GetCollider(IsoGame.Access.Directions.left);
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Right (2)") {
                col = GetCollider(IsoGame.Access.Directions.up);
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Right (3)") {
                col = GetCollider(IsoGame.Access.Directions.right);
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Left (4)") {
                col = GetCollider(IsoGame.Access.Directions.down);
            }  

        if (col != null) {
            EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
            if (col.transform.gameObject.layer == 9) {
                enemy.gameObject.GetComponent<NeutralEnemy>().Activate();
            }
            IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
        }

        IsoGame.Access.CombatManager.ReduceAttackByOne(m_Stats);      

    }    

    private Collider2D GetCollider(Vector3 direction)
    {
        Collider2D Collider;


        Collider = Physics2D.OverlapPoint(gameObject.transform.position + direction, collidableEnemies);

        if (Collider == null) {
            Collider = Physics2D.OverlapPoint((gameObject.transform.position + direction), collidableNeutralEnemies);
        }

        if (Collider != null)
        {
            return Collider;
        }    

        return Collider;
    }
}
