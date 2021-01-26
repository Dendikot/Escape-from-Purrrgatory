//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RangeEnemy : EnemyDummy
//{
//    [SerializeField]
//    private GameObject projectile;
//    [SerializeField]
//    private EnemyDummy baseEnemy;

//    private Collider2D col;

//    [SerializeField]
//    private int attackValue = 8;
//    [SerializeField]
//    private int healthValue = 25;

//    private DirectionsModel m_Directions;

//    public RangeEnemy(EnemyDummy enemy) {
//        baseEnemy = enemy;
//    }

//    void Awake()
//    {
//        baseEnemy.Stats = new Stats (attackValue, healthValue);
//        m_Directions = IsoGame.Access.Directions;
//    }

//    public void TriggerAttack() {

//        CheckAllDirections(IsoGame.Access.Directions.up, baseEnemy.CollidablePlayers);
//        CheckAllDirections(IsoGame.Access.Directions.right, baseEnemy.CollidablePlayers);
//        CheckAllDirections(IsoGame.Access.Directions.left, baseEnemy.CollidablePlayers);
//        CheckAllDirections(IsoGame.Access.Directions.down, baseEnemy.CollidablePlayers);

//        if(col != null) {
//            Attack();
//        }
//    }

//    private void CheckAllDirections(Vector3 direction, LayerMask layer) {
//        Vector3 positionToCheck;
//        Collider2D Collider = null;
//        float elapsedTime = 0f;

//        while (elapsedTime < 0.2f) {
//            positionToCheck = Vector3.Lerp(transform.position, (transform.position + (direction * 3)), (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
//            if(GetCollider(positionToCheck, layer) != null) {
//                SetCol(GetCollider(positionToCheck, layer));
//            }                   
//            elapsedTime += Time.deltaTime;                       
//        }
//    } 


//    public void Attack()
//    {
//        Transform projTransform = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Transform>();
//        StartCoroutine(SendProjectile(projTransform));

//        GameObject player = col.transform.parent.gameObject;

//        if(player.name == "Fox") {
//            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<MeleeAttack>().Stats);
//        }
//        if(player.name == "Badger") {
//            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<PushAttack>().Stats);
//        }
//        if (player.name == "Crow") {
//            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<RangeAttack>().Stats);
//        }
//        if (player.name == "Cat") {
//            IsoGame.Access.CombatManager.ReduceHealthByAttack(baseEnemy.Stats.Attack, player.GetComponent<UselessCat>().Stats);
//        } 
        
//        IsoGame.Access.CombatManager.ReduceAttackByOne(baseEnemy.Stats);

//    }

//    private IEnumerator SendProjectile(Transform projectileTransform)
//    {
//        float elapsedTime = 0f;

//        projectile.AddComponent<PolygonCollider2D>();

//        Vector3 originalPosition = projectileTransform.position;
//        Vector3 targetPosition = Vector3.zero;
      
        
//        targetPosition = col.transform.position;

//        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
//        {
//            projectileTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//        projectileTransform.position = targetPosition;

//        Destroy(projectileTransform.gameObject);
//    }

//    private Collider2D GetCollider(Vector3 direction, LayerMask layer)
//    {
//        Collider2D Collider = null;

//        Collider = Physics2D.OverlapPoint((direction), layer);


//        if (Collider != null)
//        {
//            return Collider;
//        }    

//        return Collider;
//    }

//    private void SetCol(Collider2D Collider) {
//        col = Collider;
//    }
//}