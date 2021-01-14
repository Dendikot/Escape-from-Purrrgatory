using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private LayerMask collidableNeutralEnemies;
    [SerializeField]
    private LayerMask collidableEnemies;

    private Stats m_Stats;
    public Stats Stats { get { return m_Stats; } set { m_Stats = value; } }

    [SerializeField]
    private int attackValue = 10;
    [SerializeField]
    private int healthValue = 25;    

    void Awake() {
        m_Stats = new Stats (attackValue, healthValue);
    }    


    public void Attack()
    {
        Transform projTransform = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Transform>();

        StartCoroutine(SendProjectile(projTransform));
    }

    private IEnumerator SendProjectile(Transform projectileTransform)
    {
        float elapsedTime = 0f;
        Collider2D col = null;

        Vector3 originalPosition = projectileTransform.position;
        Vector3 targetPosition = Vector3.zero;

        if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Left (1)") {
            targetPosition = originalPosition + (IsoGame.Access.Directions.left * 3);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Right (2)") {
            targetPosition = originalPosition + (IsoGame.Access.Directions.up * 3);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Right (3)") {
            targetPosition = originalPosition + (IsoGame.Access.Directions.right * 3);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Left (4)") {
            targetPosition = originalPosition + (IsoGame.Access.Directions.down * 3);
        }          
        


        while (elapsedTime < 0.2f && col == null) //this 0.2f might be interchangable
        {

            projectileTransform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);
            col = GetCollider(projectileTransform.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        projectileTransform.position = targetPosition;

        if (col != null) {
            EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
            if (col.transform.gameObject.layer == 9) {
                enemy.gameObject.GetComponent<NeutralEnemy>().Activate();
            }                                
            IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
        }

        IsoGame.Access.CombatManager.ReduceAttackByOne(m_Stats);

        Destroy(projectileTransform.gameObject);
    }

    private Collider2D GetCollider(Vector3 direction)
    {
        Collider2D Collider;

        Collider = Physics2D.OverlapPoint(direction, collidableEnemies);

        if (Collider == null) {
            Collider = Physics2D.OverlapPoint(direction, collidableNeutralEnemies);
        }


        if (Collider != null)
        {
            return Collider;
        }    

        return Collider;
    }

    //Refractor player group Movement
    //Create attack in different directions solution(maybe connected to player movement)
    //Projectile detection
}