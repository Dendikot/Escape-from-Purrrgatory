using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyDummy
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private EnemyDummy baseEnemy;

    private Collider2D col;

    [SerializeField]
    private int attackValue = 8;
    [SerializeField]
    private int healthValue = 25;

    public RangeEnemy(EnemyDummy enemy) {
        baseEnemy = enemy;
    }

    void Awake()
    {
        baseEnemy.Stats = new Stats (attackValue, healthValue);
    }

    void Update() {
        if(IsoGame.Access.TurnBased.isEnemyTurn()) {
            if(Input.GetKeyDown(KeyCode.Return)) {
                col = CheckAllDirections(IsoGame.Access.Layers.collidablePlayers);
                if(col != null) {
                    Attack();
                }
            }

        }
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


    public void Attack()
    {
        Transform projTransform = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Transform>();

        StartCoroutine(SendProjectile(projTransform));
    }

    private IEnumerator SendProjectile(Transform projectileTransform)
    {
        float elapsedTime = 0f;

        projectile.AddComponent<PolygonCollider2D>();

        Vector3 originalPosition = projectileTransform.position;
        Vector3 targetPosition = Vector3.zero;

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
        
        targetPosition = col.transform.position;

        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            projectileTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        projectileTransform.position = targetPosition;

        Destroy(projectileTransform.gameObject);

        IsoGame.Access.CombatManager.ReduceAttackByOne(baseEnemy.Stats);
    }

    private Collider2D GetCollider(Vector3 direction, LayerMask layer)
    {
        Collider2D Collider;

        Collider = Physics2D.OverlapPoint((transform.position + (direction * 3)), layer);

        if (Collider != null)
        {
            return Collider;
        }    

        return Collider;
    }
}