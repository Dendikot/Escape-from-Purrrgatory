using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralEnemy : EnemyDummy
{
 
    [SerializeField]
    private EnemyDummy baseEnemy;
    [SerializeField]
    private Sprite activeSprite;

    private Collider2D col;
    private bool isActive = false;

    [SerializeField]
    private int attackValue = 15;
    [SerializeField]
    private int healthValue = 50;

    public NeutralEnemy(EnemyDummy enemy) {
        baseEnemy = enemy;
    }

    void Awake()
    {
        baseEnemy.Stats = new Stats (attackValue, healthValue);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsoGame.Access.TurnBased.isEnemyTurn() && isActive) {
            if(Input.GetKeyDown(KeyCode.Return)) {
                col = CheckAllDirections(baseEnemy.CollidablePlayers);
                if(col != null) {
                    Attack();
                }
            }
        }
        //I might come up with a better solution but for fix HP this works
        if(isActive && gameObject.GetComponent<SpriteRenderer>().sprite != activeSprite) {
            Debug.Log("DO this only once");
            gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
    }

    public void Attack() {      
        
        GameObject player = col.transform.parent.gameObject;

        if(player.name == "Fox") {
            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<MeleeAttack>().Stats);
        }
        if(player.name == "Badger") {
            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<PushAttack>().Stats);
        }
        if (player.name == "Crow") {
            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<RangeAttack>().Stats);
        }
        if (player.name == "Cat") {
            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<UselessCat>().Stats);
        }
    

        IsoGame.Access.CombatManager.ReduceAttackByOne(baseEnemy.Stats);

    }

    private Collider2D CheckAllDirections(LayerMask layer) {
        if (GetCollider(IsoGame.Access.Directions.left, layer) != null) {
            return GetCollider(IsoGame.Access.Directions.left, layer);
        }
        else if (GetCollider(IsoGame.Access.Directions.up, layer) != null) {
            return GetCollider(IsoGame.Access.Directions.up, layer);
        }
        else if (GetCollider(IsoGame.Access.Directions.right, layer) != null) {
            return GetCollider(IsoGame.Access.Directions.right, layer);
        }
        else if (GetCollider(IsoGame.Access.Directions.down, layer) != null) {
            return GetCollider(IsoGame.Access.Directions.down, layer);
        }
        else return null;
    }    

    public void Activate() {
        isActive = true;
    }

    private Collider2D GetCollider(Vector3 direction, LayerMask layer)
    {
        Collider2D Collider;

        Collider = Physics2D.OverlapPoint(gameObject.transform.position + direction, layer);

        if (Collider != null)
        {
            return Collider;
        }    

        return Collider;
    }
}
