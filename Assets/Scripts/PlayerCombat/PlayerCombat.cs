using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCombat : MonoBehaviour
{
    protected Stats stats;
    public Stats GetStats { get { return stats; } }

    [SerializeField]
    protected int attackValue;
    [SerializeField]
    protected int healthValue;

    [SerializeField]
    protected StatusBar forceBar;
    [SerializeField]
    protected StatusBar healthBar; 

    protected LayerMask collidableEnemies;
    protected LayerMask collidablePowerUps;    

    protected CharacterGroupController groupController;

    [SerializeField]
    protected Animator anim;
    public Animator GetAnim { get { return anim; } }

    public abstract IEnumerator Attack();

    public void TriggerAttack() {
        if(groupController.PlayerTurn) {
           StartCoroutine(Attack());
           anim.SetTrigger("Attack");
        }
    }

    
    public void ReceiveDamage(int damage) {
        stats.Health -= damage;
        anim.SetTrigger("GotHit");

        if (stats.Health <= 0) {
            Die();
        }
    }

    public void Die() {
        anim.SetTrigger("Die");
    }

    public void Revive() {
        anim.SetTrigger("Revive");
    }

    public void Awake() {
        stats = new Stats(attackValue, healthValue);
        groupController = IsoGame.Access.GroupController;
        stats.ForceBar = forceBar;
        stats.HealthBar = healthBar;
        collidableEnemies = groupController.collidableEnemies;
        collidablePowerUps = groupController.collidablePowerUps;
    }

    public void FindPowerUps() {
        Collider2D powerUpCol = Physics2D.OverlapPoint(this.transform.position, collidablePowerUps);
        if (powerUpCol != null) {
            powerUpCol.transform.parent.GetComponent<PowerUp>().TriggerPowerUp(this.transform);
        }
    }
}
