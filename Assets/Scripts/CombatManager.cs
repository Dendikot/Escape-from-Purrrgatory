using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && IsoGame.Access.TurnBased.isPlayerTurn()) {
            TriggerAttacks();
            IsoGame.Access.TurnBased.IncreasePlayerActions();
        }
    }

    public void TriggerAttacks() {
        IsoGame.Access.TilePrinter.DestroyMovableTiles();
        foreach(Transform gameObject in IsoGame.Access.GroupController.GetCharacters) {
            if(gameObject.name == "Fox") {
                gameObject.GetComponent<MeleeAttack>().Attack();
            }
            if(gameObject.name == "Badger") {
                gameObject.GetComponent<PushAttack>().Attack();            
            }
            if (gameObject.name == "Crow") {
                gameObject.GetComponent<RangeAttack>().Attack();          
            }
        }
        IsoGame.Access.TilePrinter.PrintMovableTiles();
    }

    public void ReduceAttackByOne(Stats stats) 
    {
        stats.Attack -= 1;
    }

    public void ReduceHealthByAttack(int damage, Stats stats) 
    {
        stats.Health -= damage;
    }
}
