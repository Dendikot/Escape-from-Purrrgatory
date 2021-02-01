using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCombat : MonoBehaviour
{
    protected Stats stats;
    [SerializeField]
    protected int attackValue;
    [SerializeField]
    protected int healthValue;

    [SerializeField]
    protected StatusBar forceBar;
    [SerializeField]
    protected StatusBar healthBar; 

    protected CharacterGroupController groupController;

    public abstract IEnumerator Attack();

    public void TriggerAttack() {
        if(groupController.PlayerTurn) {
           StartCoroutine(Attack());
        }
    }

    
    public void ReceiveDamage(int damage) {
        stats.Health -= damage;
    }

    public void Awake() {
        stats = new Stats(attackValue, healthValue);
        groupController = IsoGame.Access.GroupController;
        stats.ForceBar = forceBar;
        stats.HealthBar = healthBar;
    }
}
