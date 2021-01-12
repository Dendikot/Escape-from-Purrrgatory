using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAttack : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemyColliders;

    private int pushedTiles;

    [SerializeField]
    private Stats m_Stats;
    public Stats stats { get { return m_Stats; } }

    void Awake() {
        m_Stats.Health = 60;
        m_Stats.Attack = 8;
    }

    public void Attack()
    {

        Collider2D col = null;

        pushedTiles = 0;

            if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Left (1)") {
                col = GetCollider(gameObject.transform.position, IsoGame.Access.Directions.left);

                if (col != null) {
                    EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
                    IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);                   
                    StartCoroutine(PushEnemy(IsoGame.Access.Directions.left, enemy));
                }   
            }
            
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Back Right (2)") {
                col = GetCollider(gameObject.transform.position, IsoGame.Access.Directions.up);

                if (col != null) {
                    EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
                    IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
                    StartCoroutine(PushEnemy(IsoGame.Access.Directions.up, enemy));                   
                }   
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Right (3)") {
                col = GetCollider(gameObject.transform.position, IsoGame.Access.Directions.right);

                if (col != null) {
                    EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
                    IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
                    StartCoroutine(PushEnemy(IsoGame.Access.Directions.right, enemy));            
                }               
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Front Left (4)") {
                col = GetCollider(gameObject.transform.position, IsoGame.Access.Directions.down);

                if (col != null) {
                    EnemyDummy enemy = (EnemyDummy)col.transform.parent.gameObject.GetComponent<EnemyDummy>();
                    IsoGame.Access.CombatManager.ReduceHealthByAttack(m_Stats.Attack, enemy.Stats);
                    StartCoroutine(PushEnemy(IsoGame.Access.Directions.down, enemy));
                    
                }   
            }

            IsoGame.Access.CombatManager.ReduceAttackByOne(m_Stats);  
    }    

    private IEnumerator PushEnemy(Vector3 direction, EnemyDummy enemy)
    {

        enemy.DestroyCollisionTiles();



        if (GetCollider(enemy.transform.position, direction) != null)
        {
            enemy.PrintCollisionTiles();
            yield break;
        }

        float elapsedTime = 0f;

        Vector3 originalPosition = enemy.transform.position;
        Vector3 targetPosition = originalPosition + direction;


        while (elapsedTime < 0.2f) //this 0.2f might be interchangable
        {
            enemy.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / 0.2f) * IsoGame.Access.LerpSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        enemy.transform.position = targetPosition;

        pushedTiles++;

        enemy.PrintCollisionTiles();

        if(pushedTiles < 2 ) {
            StartCoroutine(PushEnemy(direction, enemy));
        }

    }

    private Collider2D GetCollider(Vector3 character, Vector3 direction)
    {
        Collider2D Collider;

        Collider = Physics2D.OverlapPoint(character + direction, enemyColliders);

        if (Collider != null)
        {
            return Collider;
        }    

        return Collider;
    }
}