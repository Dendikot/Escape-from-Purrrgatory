using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            TriggerAttacks();
        }
    }

    public void TriggerAttacks() {
        IsoGame.Access.GroupController.DestroyMovableTiles();
        foreach(Transform gameObject in IsoGame.Access.GroupController.GetCharacters) {
            Debug.Log(gameObject);
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
        IsoGame.Access.GroupController.PrintMovableTiles();
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
