using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private LayerMask enemyColliders;

    [SerializeField]
    private Stats m_Stats;
    public Stats stats { get { return m_Stats; } }

    void Awake() {
        m_Stats.Health = 25;
        m_Stats.Attack = 10;
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

        projectile.AddComponent<PolygonCollider2D>();

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
            IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
        }

        Destroy(projectileTransform.gameObject);

        IsoGame.Access.CombatManager.ReduceAttackByOne(m_Stats);
    }

    private Collider2D GetCollider(Vector3 direction)
    {
        Collider2D Collider;


        Collider = Physics2D.OverlapPoint((direction), enemyColliders);

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