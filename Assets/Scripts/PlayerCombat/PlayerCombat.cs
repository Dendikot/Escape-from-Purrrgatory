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

    protected CharacterGroupController groupController;

    public abstract IEnumerator Attack();   

    public void Awake() {
        stats = new Stats(attackValue, healthValue);
        groupController = IsoGame.Access.GroupController;
    }
}
